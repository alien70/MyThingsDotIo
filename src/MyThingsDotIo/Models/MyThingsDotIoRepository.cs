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

        public IEnumerable<Person> GetAll()
        {
            _logger.LogInformation("Getting all users from database");
            return _context.Person.ToList();
        }

        public Person GetByAlias(string alias)
        {
            _logger.LogInformation($"Getting user by alias: {alias}");
            return _context.Person
                .Where(p => string.Compare(p.Alias, alias, false) == 0)
                .SingleOrDefault();
        }

        public Person GetByUniqueId(Guid uuid)
        {
            _logger.LogInformation($"Getting user by uuid: {uuid}");

            return _context.Person
                 .Where(p => p.UniqueId == uuid)
                 .SingleOrDefault();
        }

        public void Add(Person item)
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

        public void Update(Person item)
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

        public async Task<Person> Remove(string alias)
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

        public async Task<Person> Remove(Guid? uniqueId)
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
    }
}
