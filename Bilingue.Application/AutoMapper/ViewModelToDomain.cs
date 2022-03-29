using AutoMapper;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.StudentViewModels;
using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainRegistraition;
using Bilingue.Domain.DomainStudent;

namespace Bilingue.Application.AutoMapper
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            CreateMap<SaveStudentViewModel, Student>();
            CreateMap<UpdateStudentViewModel, Student>();

            CreateMap<SaveClassroomViewModel, Classroom>();

            CreateMap<SaveRegistrationViewModel, Registration>();
        }
    }
}
