using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels
{
    public class SaveStudentViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Classroom { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
