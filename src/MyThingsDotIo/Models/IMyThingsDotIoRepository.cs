using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyThingsDotIo.Models
{
    public interface IMyThingsDotIoRepository
    {
        IEnumerable<Person> GetAll();
        Person GetByAlias(string alias);
        Person GetByUniqueId(Guid uuid);

        void Update(Person item);
        void Add(Person item);
        Task<Person> Remove(string alias);
        Task<Person> Remove(Guid? uuid);

        Task<bool> SaveChangesAsync();
    }
}