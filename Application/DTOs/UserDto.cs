using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required, MaxLength(25), MinLength(2)]
        public string UserName { get; set; }

        [Required, MaxLength(30), MinLength(5)]
        public string PassWord { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}
