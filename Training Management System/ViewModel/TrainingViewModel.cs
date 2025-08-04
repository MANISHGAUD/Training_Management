using System.ComponentModel.DataAnnotations;
using Training_Management_System.Helpers;

namespace Training_Management_System.ViewModel
{
    public class TrainingViewModel
    {
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Training Date")]
        [CustomValidation(typeof(DateValidator), "ValidateCurrentOrFutureDate")]
        public DateTime TrainingDate { get; set; }

        public string Place { get; set; }
        public string Purpose { get; set; }

        [Required]
        public List<int> SelectedEmployeeIds { get; set; }
    }
}
