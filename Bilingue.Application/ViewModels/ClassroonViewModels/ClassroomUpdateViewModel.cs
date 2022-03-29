using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels
{
    public class ClassroomUpdateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }
        public int SchoolYear { get; set; }
    }
}
