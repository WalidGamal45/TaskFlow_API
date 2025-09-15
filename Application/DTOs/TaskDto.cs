using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public  class TaskDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? Deadline { get; set; }
        public bool IsCompleted { get; set; }

    }
}
