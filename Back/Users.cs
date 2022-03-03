using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Back
{
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "username field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string username { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public char  Role { get; set; }
    }
}
