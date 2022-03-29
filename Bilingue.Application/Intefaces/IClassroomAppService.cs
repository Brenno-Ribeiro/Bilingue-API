using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.ClassroonViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Application.Intefaces
{
    public interface IClassroomAppService
    {
        Task<List<ClassroomResponseViewModel>> GetAllClassroomAsync();
        Task<bool> InsertClassroom(string number);
        Task<bool> UpdateClassroom(ClassroomUpdateViewModel classroomUpdateViewModel);
        Task<bool> DeleteClassroom(Guid id);

        Task<bool> ClassroomExist(string number);
        Task<int> VerifyQuantityStudentInClassroom(string number);
        Task<int> VerifyQuantityStudentInClassroom(Guid id);

        Task<ClassroomResponseViewModel> GetClassroomByNumber(string number);
        Task<Guid> GetGuidClassroomByNumber(string number);
    }
}
