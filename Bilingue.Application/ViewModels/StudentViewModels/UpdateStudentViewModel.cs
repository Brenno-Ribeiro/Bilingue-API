using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels.StudentViewModels
{
    public class UpdateStudentViewModel
    {   
        
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
