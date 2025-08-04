using System;
using System.Collections.Generic;

namespace Training_Management_System.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public int? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<TrainingEmployee> TrainingEmployees { get; set; } = new List<TrainingEmployee>();
}
