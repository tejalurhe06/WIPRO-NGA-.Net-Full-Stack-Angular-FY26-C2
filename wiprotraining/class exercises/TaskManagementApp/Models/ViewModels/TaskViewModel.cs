using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}