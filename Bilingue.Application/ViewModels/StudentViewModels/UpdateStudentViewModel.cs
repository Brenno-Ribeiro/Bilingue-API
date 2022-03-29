using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels.StudentViewModels
{
    public class UpdateStudentViewModel
    {   
        public string Name { get; set; }
        public string Cpf { get; set; }   
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
