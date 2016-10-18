using System;

namespace MyThingsDotIo.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int UserId { get; set; }

        public string Value { get; set; }

        public ContactType Type { get; set; }

        public bool Default { get; set; } = false;

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
