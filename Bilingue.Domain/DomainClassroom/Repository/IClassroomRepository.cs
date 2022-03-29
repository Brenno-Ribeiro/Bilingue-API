using Bilingue.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace Bilingue.Domain.DomainClassroom.Repository
{
    public  interface IClassroomRepository : IBaseRepository<Classroom>
    {
        Task<int> VerifyQuantityStudentInClassroom(string number);
        Task<int> VerifyQuantityStudentInClassroom(Guid id);
        Task<bool> ClassroomExist(string number);
        Task<bool> ClassroomExist(Guid guid);
        Task<Classroom> GetClassroomByNumber(string number);
        Task<Guid> GetGuidClassroomByNumber(string number);
    }
}
