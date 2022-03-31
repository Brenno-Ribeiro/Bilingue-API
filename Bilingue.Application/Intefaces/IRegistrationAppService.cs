using Bilingue.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace Bilingue.Application.Intefaces
{
    public interface IRegistrationAppService
    {
        Task<RegistrationResponseWithListViewModel> GetRegistrationAsync(Guid id);
        Task<RegistrationResponseNoListViewModel> GetRegistrationAsync(string CPF,string classnumber);
        Task<RegistrationResponseNoListViewModel> GetRegistrationAsync(Guid studentId, Guid classroomId);

        Task<bool> RegistrationExist(Guid studentId, Guid classroomId);

        Task<bool> DeleteRegistration(Guid studentId, Guid classroomId);
        Task<bool> DeleteRegistration(Guid studentId);
       
        Task<bool> InsertRegistration(string CPF, string classrromNumber);
        Task<bool> InsertRegistration(Guid studentId, Guid classroomId);
    }
}
