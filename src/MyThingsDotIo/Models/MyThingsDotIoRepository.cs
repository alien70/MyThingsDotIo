using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyThingsDotIo.Models
{
    public class MyThingsDotIoRepository : IMyThingsDotIoRepository
    {
        private MyThingsDotIoContext _context;

        public MyThingsDotIoRepository(MyThingsDotIoContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();            
        }
    }
}
