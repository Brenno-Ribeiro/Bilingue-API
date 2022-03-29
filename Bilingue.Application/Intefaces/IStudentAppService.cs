using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.StudentViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Application.Intefaces
{
    public interface IStudentAppService
    {
        Task<List<StudentResponseViewModel>> GetAllStudentAsync();

        Task<bool> InsertStudent(SaveStudentViewModel model);
        Task<bool> UpdateStudent(Guid id, UpdateStudentViewModel model);

        Task<bool> DeleteStudent(Guid id);

        Task<bool> StudentExist(string CPF);
        Task<StudentResponseViewModel> GetStudentAsyncByCpf(string CPF);
        Task<Guid> GetGuidStudentAsyncByCpf(string CPF);
    }
}
