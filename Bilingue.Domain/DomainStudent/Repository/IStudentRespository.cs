using Bilingue.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Domain.DomainStudent.Repository
{
    public interface IStudentRespository : IBaseRepository<Student> 
    {
        Task<bool> StudentExist(string CPF);
        Task<Student> GetStudentAsyncByCpf(string CPF);
        Task<Guid> GetGuidStudentAsyncByCpf(string CPF);
    }
}
