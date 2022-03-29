using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilingue.Application.ViewModels
{
    public class SaveClassroomViewModel
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public int SchoolYear { get; set; }
    }
}
