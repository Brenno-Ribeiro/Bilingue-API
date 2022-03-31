using AutoMapper;
using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.ClassroonViewModels;
using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainClassroom.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Application.Services
{
    public class ClassroomAppService : IClassroomAppService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IMapper _mapper;


        public ClassroomAppService(IClassroomRepository classroomRepository, IMapper mapper)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
        }


        public async Task<bool> ClassroomExist(string number)
        {
            return await _classroomRepository.ClassroomExist(number);
        }

        public async Task<bool> ClassroomExist(Guid id)
        {
            return await _classroomRepository.ClassroomExist(id);
        }

        public async Task<bool> ClassroomExist(string currentClassroom, string transferTo)
        {
            return await _classroomRepository.ClassroomExist(currentClassroom,transferTo);
        }

        public async Task<bool> DeleteClassroom(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            return await _classroomRepository.DeleteAsync(classroom);
        }


        public async Task<List<ClassroomResponseViewModel>> GetAllClassroomAsync()
        {
            var classrooms = await _classroomRepository.GetAllAsync();

            var list = new List<ClassroomResponseViewModel>();

            foreach (var item in classrooms)
            {
                list.Add(_mapper.Map<ClassroomResponseViewModel>(item));
            }

            return list;
        }


        public async Task<ClassroomResponseViewModel> GetClassroomByNumber(string number)
        {
            var classroom = await _classroomRepository.GetClassroomByNumber(number);
            return _mapper.Map<ClassroomResponseViewModel>(classroom);
        }


        public async Task<Guid> GetGuidClassroomByNumber(string number)
        {
            return await _classroomRepository.GetGuidClassroomByNumber(number);
        }


        public async Task<bool> InsertClassroom(SaveClassroomViewModel model)
        {
            var classroom = _mapper.Map<Classroom>(model);
            return await _classroomRepository.SaveAsync(classroom);
        }


        public async Task<bool> UpdateClassroom(Guid id, ClassroomUpdateViewModel model)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            _mapper.Map(model, classroom);
            return await _classroomRepository.UpdateAsync(classroom);
        }


        public async Task<int> VerifyQuantityStudentInClassroom(string number)
        {
            return await _classroomRepository.VerifyQuantityStudentInClassroom(number);
        }


        public async Task<int> VerifyQuantityStudentInClassroom(Guid id)
        {
            return await _classroomRepository.VerifyQuantityStudentInClassroom(id);
        }
    }
}
