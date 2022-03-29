using System;
using System.ComponentModel.DataAnnotations;

namespace Bilingue.Application.ViewModels
{
    public class ClassroomUpdateViewModel
    {
        [Required]
        public string OldNumber { get; set; }
        [Required]
        public string NewNumber { get; set; }
        public int SchoolYear { get; set; }
    }
}
