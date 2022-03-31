using Bilingue.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Domain.DomainRegistraition.Repository
{
    public interface IRegistrationRepository : IBaseRepository<Registration>
    {
        Task<IEnumerable<Registration>> GetRegistrationsJoins(Guid id);

        Task<bool> RegistrationExist(Registration registration);
        Task<List<Registration>> GetRegistrationRage(Guid id);


        Task<Registration> GetRegistration(Guid studentId, Guid classroomId);
        Task<Registration> GetRegistration(string CPF, string classromNumber);

         Guid GetGuidRegistration(Guid studentId, Guid classroomId);
    }
}
