using MyThingsDotIo.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyThingsDotIo.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Value { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        public Guid UniqueId { get; set; }

        public bool Default { get; set; }
    }
}
