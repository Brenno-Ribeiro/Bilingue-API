using Bilingue.Domain.DomainStudent;
using Bilingue.Domain.DomainStudent.Repository;
using Bilingue.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bilingue.Infra.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRespository
    {
        public StudentRepository(BilingueContext context) : base(context)
        {
        }


        public async Task<Guid> GetGuidStudentAsyncByCpf(string CPF)
        {
            var studentId = Guid.Empty;

            try
            {
                var student = await _context.Students
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Cpf.Equals(CPF));
                studentId = student.Id;
            }
            catch (Exception e)
            {

                e.Message.ToString();
            }

            return studentId;
        }

        public async Task<Student> GetStudentAsyncByCpf(string CPF)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Cpf.Equals(CPF));
        }


        public async Task<bool> StudentExist(Guid id)
        {
            return await _context.Students.AnyAsync(x => x.Id.Equals(id));
        }



        public async Task<bool> StudentExist(string CPF)
        {
            return await _context.Students.AnyAsync(x => x.Cpf.Equals(CPF));
        }
        
    }
}
