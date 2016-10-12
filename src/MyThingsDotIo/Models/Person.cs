using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyThingsDotIo.Models
{
    public class Person
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
