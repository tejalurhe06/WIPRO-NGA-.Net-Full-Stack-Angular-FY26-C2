using System;
using System.Collections.Generic;

namespace DatabaseFirst_EFCORE.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> EmployeeDepartmentId1Navigations { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> EmployeeDepartments { get; set; } = new List<Employee>();
}
