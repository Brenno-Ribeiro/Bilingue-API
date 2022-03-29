using Bilingue.Domain.DomainRegistraition;
using Bilingue.Domain.DomainRegistraition.Repository;
using Bilingue.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilingue.Infra.Repository
{
    public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(BilingueContext context) : base(context)
        {
        }

        public async Task<Registration> GetRegistration(Guid studentId, Guid classroomId)
        {
            return await _context.Registrations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId.Equals(studentId) && x.ClassroomId.Equals(classroomId));
        }

        public async Task<Registration> GetRegistration(string CPF, string classromNumber)
        {
            return await _context.Registrations
                .AsNoTracking()
                .Include("Student")
                .Include("Classroom")
                .FirstOrDefaultAsync(x => x.Student.Cpf.Equals(CPF) && x.Classroom.Number.Equals(classromNumber));
        }

        public async Task<List<Registration>> GetRegistrationRage(Guid id)
        {
            return await _context.Registrations
                .AsNoTracking()
                .Where(x => x.Student.Id.Equals(id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsJoins(Guid id)
        {
            return await _context.Registrations
                .AsNoTracking()
                .Include("Student")
                .Include("Classroom")
                .Where(x => x.Student.Id.Equals(id))
                .ToListAsync();
        }

        public async Task<bool> RegistrationExist(Registration registration)
        {
            return await _context.Registrations
                .AnyAsync(x => x.ClassroomId.Equals(registration.ClassroomId) && x.StudentId.Equals(registration.StudentId));
        }
    }
}
