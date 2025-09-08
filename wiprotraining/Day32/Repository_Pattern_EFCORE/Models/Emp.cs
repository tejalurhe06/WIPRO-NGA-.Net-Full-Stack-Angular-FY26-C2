using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RepositoryPatternEFCore.Models
{
    [Table("Employees")]
    public class Emp
    {
        [Key]
        public int EmpId { get; set; }

        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Employee Name is required")]
        public string EmpName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}