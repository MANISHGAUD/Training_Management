using System;
using System.Collections.Generic;

namespace Training_Management_System.Models;

public partial class Training
{
    public int TrainingId { get; set; }
    public int OrganizationId { get; set; }
    public DateTime TrainingDate { get; set; }
    public string? Place { get; set; }
    public string? Purpose { get; set; }

    public virtual Organization Organization { get; set; } = null!;
    public virtual ICollection<TrainingEmployee> TrainingEmployees { get; set; } = new List<TrainingEmployee>();
}
