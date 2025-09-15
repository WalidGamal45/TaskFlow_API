using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
