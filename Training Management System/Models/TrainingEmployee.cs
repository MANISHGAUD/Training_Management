using System;
using System.Collections.Generic;

namespace Training_Management_System.Models;

public partial class TrainingEmployee
{
    public int TrainingEmployeeId { get; set; }

    public int? TrainingId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Training? Training { get; set; }
}
