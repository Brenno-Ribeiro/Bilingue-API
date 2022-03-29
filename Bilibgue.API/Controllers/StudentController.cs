using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.StudentViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilibgue.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentAppService _studentAppService;
        private readonly IClassroomAppService _classroomAppService;
        private readonly IRegistrationAppService _registrationAppService;

        public StudentController(IStudentAppService studentAppService, IClassroomAppService classroomAppService, IRegistrationAppService registrationAppService)
        {
            _studentAppService = studentAppService;
            _classroomAppService = classroomAppService;
            _registrationAppService = registrationAppService;
        }


        /// <summary>
        ///    Busca todos os alunos cadastrados na base de dados
        /// </summary>
        /// <response code="200">Lista todos os alunos cadastrados</response>
        /// <response code="401">Lista não encontrada</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<StudentResponseViewModel>), 200)]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await _studentAppService.GetAllStudentAsync();

            if (students.Count.Equals(0))
            {
                return StatusCode(404, new { message = "Não há alunos cadastrados" });
            }

            return StatusCode(200, new { Students = students });
        }

        /// <summary>
        ///     Cadastra um aluno na base de dados
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Retorna os dados do aluno e sua turma</response>
        /// <response code="400">Retorna uma mensagem de requisição mal sucedida</response>
        [HttpPost]
        [ProducesResponseType(typeof(RegistrationResponseNoListViewModel), 201)]
        public async Task<IActionResult> SaveStudent(SaveStudentViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(400, new { message = "Por favor insrire dados validos!" });
            }

            if (await _studentAppService.StudentExist(model.Cpf))
            {
                return StatusCode(400, new { message = "O CPF informado já existe em nossa base de dados!" });
            }


            if (!await _classroomAppService.ClassroomExist(model.Classroom))
            {
                return StatusCode(404, new { message = "Essa turma ainda não existe no banco de dados!" });
            }

            var quantityStudentInClassroom = await _classroomAppService.VerifyQuantityStudentInClassroom(model.Classroom);

            if (quantityStudentInClassroom > 5)
            {
                return StatusCode(400, new { message = "Essa turma está cheia!" });
            }

            var studentDone = await _studentAppService.InsertStudent(model);

            if (!studentDone)
            {
                return StatusCode(500, new { message = "Um erro ocoreeu no nosso servidor, estamos averiguando......" });
            }

            var registrationDone = await _registrationAppService.InsertRegistration(model.Cpf, model.Classroom);

            if (!registrationDone)
            {
                return StatusCode(500, new { message = "Um erro ocoreeu no nosso servidor, estamos averiguando......" });
            }

            var student = await _registrationAppService.GetRegistrationAsync(model.Cpf, model.Classroom);
            return StatusCode(201, new { Student = student });
        }


        /// <summary>
        ///     Atualiza um aluno na base de dados
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        ///  <response code="200">Retorna os dados atualizados</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StudentResponseViewModel),200)]
        public async Task<IActionResult> UpdateStudent(Guid id ,UpdateStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Por Favor, insire dados validos!!");
            }

            if (!await _studentAppService.StudentExist(id))
            {
                return StatusCode(404, new { Response = "Aluno não existe!" });
            }

            var result = await _studentAppService.UpdateStudent(id,model);

            if (!result)
            {
                return BadRequest("Erro ao atualizar!");
            }

            return StatusCode(200, new { Response = "Aluno atualizado com sucesso" });
        }


        /// <summary>
        ///     Exclui um aluno na base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Por Favor, insire dados validos!!");
            }

            var deleteregistrations = await _registrationAppService.DeleteRegistration(id);
            var student = await _studentAppService.DeleteStudent(id);

            if (!student && deleteregistrations)
            {
                return BadRequest("Ops! ocorreu um erro, estamos verificando.....");
            }

            return StatusCode(200, new { Response = "Aluno deletado com sucesso!" });
        }
    }
}
