using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Task1> Tasks { get; set; }
    }
}
