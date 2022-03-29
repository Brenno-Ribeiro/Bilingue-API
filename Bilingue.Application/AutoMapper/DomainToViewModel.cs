using AutoMapper;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.ClassroonViewModels;
using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainRegistraition;
using Bilingue.Domain.DomainStudent;

namespace Bilingue.Application.AutoMapper
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Student, StudentResponseViewModel>();
            CreateMap<Classroom, ClassroomResponseViewModel>();
            CreateMap<Classroom, ClassroomUpdateViewModel>();
            CreateMap<Registration, RegistrationResponseWithListViewModel>();
        }
    }
}
