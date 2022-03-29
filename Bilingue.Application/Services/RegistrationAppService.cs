using AutoMapper;
using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.ClassroonViewModels;
using Bilingue.Domain.DomainClassroom.Repository;
using Bilingue.Domain.DomainRegistraition;
using Bilingue.Domain.DomainRegistraition.Repository;
using Bilingue.Domain.DomainStudent.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Application.Services
{
    public class RegistrationAppService : IRegistrationAppService
    {

        private readonly IRegistrationRepository _registrationRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IStudentRespository _studentRepository;
        private readonly IMapper _mapper;

        public RegistrationAppService(IRegistrationRepository registrationRepository, IClassroomRepository classroomRepository, IStudentRespository studentRepository, IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _classroomRepository = classroomRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteRegistration(Guid studentId, Guid classroomId)
        {
            var registration = await _registrationRepository.GetRegistration(studentId, classroomId);
            return await _registrationRepository.DeleteAsync(registration);
        }

        public async Task<bool> DeleteRegistration(Guid studentId)
        {
            var registrations = await _registrationRepository.GetRegistrationRage(studentId);
            return await _registrationRepository.DeleteAsyncRange(registrations);
        }

      
        public async Task<RegistrationResponseWithListViewModel> GetRegistrationAsync(Guid id)
        {
            var registrations = await _registrationRepository.GetRegistrationsJoins(id);
            var registrationResponse = new RegistrationResponseWithListViewModel();

            foreach (var item in registrations)
            {
                registrationResponse.Student = _mapper.Map<StudentResponseViewModel>(item.Student);
                registrationResponse.Classrooms.Add(_mapper.Map<ClassroomResponseViewModel>(item.Classroom));
            }

            return registrationResponse;
        }

        public async Task<RegistrationResponseNoListViewModel> GetRegistrationAsync(string CPF, string classnumber)
        {
            var registration = await _registrationRepository.GetRegistration(CPF, classnumber);

            var registrationResponse = new RegistrationResponseNoListViewModel();

            registrationResponse.Student = _mapper.Map<StudentResponseViewModel>(registration.Student);
            registrationResponse.Classroom = _mapper.Map<ClassroomResponseViewModel>(registration.Classroom);

            return registrationResponse;
        }

        public async Task<bool> InsertRegistration(Guid studentId, Guid classroomId)
        {
            var registration = new Registration
            {
                StudentId = studentId,
                ClassroomId = classroomId
            };

            return await _registrationRepository.SaveAsync(registration);
        }

        public async Task<bool> InsertRegistration(string CPF, string classroomNumber)
        {
            var studentId = await _studentRepository.GetGuidStudentAsyncByCpf(CPF);
            var classroomId = await _classroomRepository.GetGuidClassroomByNumber(classroomNumber);

            var registration = new Registration
            {
                StudentId = studentId,
                ClassroomId = classroomId
            };

            return await _registrationRepository.SaveAsync(registration);
        }

        public async Task<bool> RegistrationExist(Guid studentId, Guid classroomId)
        {
            var registration = new Registration
            {
                StudentId = studentId,
                ClassroomId = classroomId
            };

            return await _registrationRepository.RegistrationExist(registration);
        }
    }
}
