using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels.StudentViewModels
{
    public class DeleteStudentViewModel
    {
        [Required]
        public string CPF { get; set; }
    }
}
