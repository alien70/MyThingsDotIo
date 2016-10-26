using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyThingsDotIo.Models
{
    public interface IMyThingsDotIoRepository
    {
        #region User

        IEnumerable<User> GetAll();
        User GetByAlias(string alias);
        User GetByUniqueId(Guid uuid);

        void Update(User item);
        void Add(User item);
        Task<User> RemoveUser(string alias);
        Task<User> RemoveUser(Guid? uuid);

        Task<bool> SaveChangesAsync();

        #endregion

        #region Contacts

        IEnumerable<Contact> GetContactsByAlias(string alias);
        void Add(string alias, Contact item);
        Task<Contact> RemoveContact(Guid? uuid);
        #endregion

    }
}