using System;
using System.ComponentModel.DataAnnotations;

namespace MyThingsDotIo.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Alias { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Guid UniqueId { get; set; }
    }
}
