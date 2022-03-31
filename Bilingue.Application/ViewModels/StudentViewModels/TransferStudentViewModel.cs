using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels.StudentViewModels
{
    public class TransferStudentViewModel
    {
        [Required]
        [MaxLength(50)]
        public string CurrentClassroom { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransferTo { get; set; }
    }
}
