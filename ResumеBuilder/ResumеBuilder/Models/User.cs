using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ResumеBuilder.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("Full Name")]
        [Required]
        public string? FullName { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string? Password { get; set; }
    }
}
