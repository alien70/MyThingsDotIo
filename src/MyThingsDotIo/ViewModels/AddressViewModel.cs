using MyThingsDotIo.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyThingsDotIo.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Value { get; set; }

        [Required]
        public AddressType Type { get; set; }

        [Required]
        public Guid UniqueId { get; set; }
    }
}
