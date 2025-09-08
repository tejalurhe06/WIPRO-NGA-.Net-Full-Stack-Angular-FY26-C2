using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int DepartmentId { get; set; } // Foreign key property
        public Department? Department { get; set; }
    
    }
}
