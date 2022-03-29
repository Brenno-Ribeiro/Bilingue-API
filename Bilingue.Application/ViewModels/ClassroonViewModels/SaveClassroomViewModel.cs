using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels
{
    public class SaveClassroomViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [Required]
        public int SchoolYear { get; set; }
    }
}
