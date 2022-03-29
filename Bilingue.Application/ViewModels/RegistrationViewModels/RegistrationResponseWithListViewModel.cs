using Bilingue.Application.ViewModels.ClassroonViewModels;
using System.Collections.Generic;

namespace Bilingue.Application.ViewModels
{
    public class RegistrationResponseWithListViewModel
    {
        public RegistrationResponseWithListViewModel()
        {
           Classrooms = new HashSet<ClassroomResponseViewModel>();
        }

        public StudentResponseViewModel Student { get; set; }
        public ICollection<ClassroomResponseViewModel> Classrooms { get; set; }
    }
}
