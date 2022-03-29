using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bilibgue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationAppService _registrationAppService;
        private readonly IClassroomAppService _classroomAppService;
        private readonly IStudentAppService _studentAppService;

        public RegistrationController(IRegistrationAppService registrationAppService, IClassroomAppService classroomAppService, IStudentAppService studentAppService)
        {
            _registrationAppService = registrationAppService;
            _classroomAppService = classroomAppService;
            _studentAppService = studentAppService;
        }

        /// <summary>
        ///     Volta a matricula completa do aluno baseada no id do mesmo
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>

        [HttpGet("{studentId}")]
        [ProducesResponseType(typeof(RegistrationResponseWithListViewModel),200)]
        public async Task<IActionResult> GetAllRegistrationById(Guid studentId)
        {
            var registrationResponse = await _registrationAppService.GetRegistrationAsync(studentId);

            if (registrationResponse.Classrooms.Count.Equals(0))
            {
                return NotFound("Nenhum registro encontrado!");
            }

            return StatusCode(200, new { Response = registrationResponse });
        }

        /// <summary>
        ///     Cadastra uma matricula no bando de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegistrationResponseNoListViewModel),201)]
        public async Task<IActionResult> SaveRegistration(RegistrationRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Por favor! insire dados validos!");
            }

            var studentId = await _studentAppService.GetGuidStudentAsyncByCpf(model.CPF);
            var classroomId = await _classroomAppService.GetGuidClassroomByNumber(model.ClassroomNumber);


            if (studentId.Equals(Guid.Empty) || classroomId.Equals(Guid.Empty))
            {
                return NotFound("CPF ou Turma não existe no banco de dados!");
            }

            var quantityStudentInClassroom = await _classroomAppService.VerifyQuantityStudentInClassroom(model.ClassroomNumber);

            if (quantityStudentInClassroom > 5)
            {
                return BadRequest("Essa turma está cheia");
            }

            if (await _registrationAppService.RegistrationExist(studentId, classroomId))
            {
                return BadRequest("Esse Aluno já está matriculado nessa turma");
            }

            var result = await _registrationAppService.InsertRegistration(studentId,classroomId);

            if (!result)
            {
                return BadRequest("Ops! ocorreu um erro, não se preocupe estamos averiguando....");
            }

            var registrationResponse = await _registrationAppService.GetRegistrationAsync(model.CPF, model.ClassroomNumber);
            return StatusCode(201, new { Registration = registrationResponse });
        }


        /// <summary>
        ///     Exclui uma matricula especifica no banco de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistration(RegistrationRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Por favor! insire dados validos!");
            }

            var studentId = await _studentAppService.GetGuidStudentAsyncByCpf(model.CPF);
            var classroomId = await _classroomAppService.GetGuidClassroomByNumber(model.ClassroomNumber);

            if (studentId == Guid.Empty || classroomId == Guid.Empty)
            {
                return BadRequest("CPF ou Turma não estão cadastrado no sistema");
            }

            var result = await _registrationAppService.DeleteRegistration(studentId, classroomId);

            if (!result)
            {
                return BadRequest("Ops! ocorreu um erro, não se preocupe estamos averiguando....");
            }

            return StatusCode(200, new { Registration = "Matricula excluida com sucesso!" });
        }

    }
}
