using System;
using System.Collections.Generic;

namespace DatabaseFirst_EFCORE.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int? DepartmentId1 { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Department? DepartmentId1Navigation { get; set; }
}
