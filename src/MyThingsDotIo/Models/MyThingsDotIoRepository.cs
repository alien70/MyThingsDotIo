using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyThingsDotIo.Models
{
    public class MyThingsDotIoRepository : IMyThingsDotIoRepository
    {
        private MyThingsDotIoContext _context;
        private ILogger<MyThingsDotIoContext> _logger;

        public MyThingsDotIoRepository(MyThingsDotIoContext context, ILogger<MyThingsDotIoContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region User

        public IEnumerable<User> GetAll()
        {
            _logger.LogInformation("Getting all users from database");
            return _context.Person
                .Include(t => t.Addresses)
                .Include(t => t.Contacts)
                .ToList();
        }


        public User GetByAlias(string alias)
        {
            _logger.LogInformation($"Getting user by alias: {alias}");
            return _context.Person
                .Where(p => string.Compare(p.Alias, alias, false) == 0)
                .Include(t => t.Addresses)
                .Include(t => t.Contacts)
                .OrderBy(t => t.Alias)
                .FirstOrDefault();
        }

        public User GetByUniqueId(Guid uuid)
        {
            _logger.LogInformation($"Getting user by uuid: {uuid}");

            return _context.Person
                 .Where(p => p.UniqueId == uuid)
                .Include(t => t.Addresses)
                .Include(t => t.Contacts)
                .OrderBy(t => t.Alias)
                .FirstOrDefault();
        }

        public void Add(User item)
        {
            _logger.LogInformation("Adding new user to database");

            item.UniqueId = Guid.NewGuid();
            item.Created = DateTime.Now;

            _context.Person.Add(item);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation("Saving to the database");

            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update(User item)
        {
            _logger.LogInformation($"Updating item {item.UniqueId}");
            var toUpdate = _context.Person.SingleOrDefault(p => p.UniqueId == item.UniqueId);
            if (toUpdate != null)
            {
                toUpdate.FirstName = item.FirstName;
                toUpdate.LastName = item.LastName;
                toUpdate.Alias = item.Alias;
                toUpdate.DateOfBirth = item.DateOfBirth;
                toUpdate.Modified = DateTime.Now;
            }
        }

        public async Task<User> Remove(string alias)
        {
            var person = await _context.Person
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.Alias == alias);

            if (person != null)
            {
                try
                {
                    _context.Person.Remove(person);
                    await SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to remove person: {ex}");
                }
            }

            return person;
        }

        public async Task<User> Remove(Guid? uniqueId)
        {
            if (!uniqueId.HasValue)
                return null;

            var person = await _context.Person
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.UniqueId == uniqueId);

            if (person != null)
            {
                try
                {
                    _context.Person.Remove(person);
                    await SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to remove person: {ex}");
                }
            }

            return person;
        }

        #endregion

        #region Contacts

        public IEnumerable<Contact> GetContactsByAlias(string alias)
        {
            _logger.LogInformation($"Getting user contacts by alias: {alias}");

            var contacts = _context.Person
                .Where(p => string.Compare(p.Alias, alias, true) == 0)
                .Include(t => t.Contacts)
                .Select(p => p.Contacts)
                .SingleOrDefault();

            return contacts.OrderByDescending(c => c.Default ? 1 : 0);
        }

        public void Add(string alias, Contact item)
        {
            _logger.LogInformation($"Adding contact to {alias}");
            var user = GetByAlias(alias);

            if (user != null)
            {
                item.UserId = user.Id;
                item.UniqueId = Guid.NewGuid();
                item.Created = DateTime.Now;

                user.Contacts.Add(item);
                _context.Contacts.Add(item);
            }
        }

        public async Task<Contact> RemoveContact(Guid? uniqueId)
        {
            var contact = await _context.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.UniqueId == uniqueId);

            if (contact != null)
            {
                try
                {
                    _context.Contacts.Remove(contact);
                    await SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to remove contact: {ex}");
                }
            }

            return contact;
        }

        #endregion
    }
}
