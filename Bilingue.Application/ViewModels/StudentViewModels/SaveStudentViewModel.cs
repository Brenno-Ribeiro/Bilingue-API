using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels
{
    public class SaveStudentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Classroom { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
