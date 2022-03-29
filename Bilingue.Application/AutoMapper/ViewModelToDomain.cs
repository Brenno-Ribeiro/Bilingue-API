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

            CreateMap<UpdateStudentViewModel, Student>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != "string")))
                .ForMember(dest => dest.Cpf, opt => opt.Condition(src => (src.Cpf != "string")))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => (src.Email != "string")))
                .ForMember(dest => dest.Age, opt => opt.Condition(src => (src.Age != 0)));


            CreateMap<ClassroomUpdateViewModel, Classroom>()
                .ForMember(dest => dest.Number, opt => opt.Condition(src => (src.Number != "string")))
                .ForMember(dest => dest.SchoolYear, opt => opt.Condition(src => (src.SchoolYear != 0)));

            CreateMap<SaveClassroomViewModel, Classroom>();

            CreateMap<SaveRegistrationViewModel, Registration>();
        }
    }
}
