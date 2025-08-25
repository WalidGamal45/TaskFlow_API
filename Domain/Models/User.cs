using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
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

        public string VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiry { get; set; }
        public bool IsConfirmed { get; set; }
        public ICollection<Task1> Tasks { get; set; }
    }
}
