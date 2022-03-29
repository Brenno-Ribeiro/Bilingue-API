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
            //todo: Refatorar a logíca de update do aluno
            var properties = model.GetType().GetProperties();
            var values = new Dictionary<string, string>();

            for (int i = 0; i < properties.Length; i++)
            {
                var property = GetPropValue(model, properties[i].Name);

                if (property.Equals("string") || property.Equals(0))
                {
                    continue;
                }

                values.Add(properties[i].Name, property.ToString());
            }

            var student = _mapper.Map<Student>(values);
            student.Id = id;    

            return await _studentRespository.UpdateAsync(student);

        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
