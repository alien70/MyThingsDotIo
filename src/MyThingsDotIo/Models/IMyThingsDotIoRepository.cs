using System.Collections.Generic;

namespace MyThingsDotIo.Models
{
    public interface IMyThingsDotIoRepository
    {
        IEnumerable<User> GetAllUsers();
    }
}