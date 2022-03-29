using AutoMapper;
using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.StudentViewModels;
using Bilingue.Domain.DomainStudent;
using Bilingue.Domain.DomainStudent.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Application.Services
{
    public class StudentAppService : IStudentAppService
    {
        private readonly IStudentRespository _studentRespository;
        private readonly IMapper _mapper;

        public StudentAppService(IStudentRespository studentRespository, IMapper mapper)
        {
            _studentRespository = studentRespository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            var student = await _studentRespository.GetByIdAsync(id);
            return await _studentRespository.DeleteAsync(student);
        }

        public async Task<List<StudentResponseViewModel>> GetAllStudentAsync()
        {
            var students = await _studentRespository.GetAllAsync();

            var list = new List<StudentResponseViewModel>();

            foreach (var item in students)
            {
                list.Add(_mapper.Map<StudentResponseViewModel>(item));
            }

            return list;
        }

        public async Task<Guid> GetGuidStudentAsyncByCpf(string CPF)
        {
            return await _studentRespository.GetGuidStudentAsyncByCpf(CPF);
        }

        public async Task<StudentResponseViewModel> GetStudentAsyncByCpf(string CPF)
        {
            var student = await _studentRespository.GetStudentAsyncByCpf(CPF);
            return _mapper.Map<StudentResponseViewModel>(student);
        }

        public async Task<bool> InsertStudent(SaveStudentViewModel saveStudentViewModel)
        {
            var student = _mapper.Map<Student>(saveStudentViewModel);
            return await _studentRespository.SaveAsync(student);
        }

        public Task<bool> StudentExist(string CPF)
        {
            return _studentRespository.StudentExist(CPF);
        }

        public async Task<bool> UpdateStudent(Guid id, UpdateStudentViewModel model)
        {
            var student = await _studentRespository.GetByIdAsync(id);
            _mapper.Map(model, student);
            return await _studentRespository.UpdateAsync(student);
        }


        public async Task<bool> StudentExist(Guid id)
        {
            return await _studentRespository.StudentExist(id);
        }
    }
}
