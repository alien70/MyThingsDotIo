using System;
using System.Collections;
using System.Collections.Generic;

namespace MyThingsDotIo.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
