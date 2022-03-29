using Bilingue.Application.Intefaces;
using Bilingue.Application.ViewModels;
using Bilingue.Application.ViewModels.ClassroonViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilibgue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomAppService _classroomAppService;

        public ClassroomController(IClassroomAppService classroomAppService)
        {
            _classroomAppService = classroomAppService;
        }


        /// <summary>
        ///     Busca todas as turmas no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClassroomResponseViewModel>), 200)]
        public async Task<IActionResult> GetAllClassroom()
        {
            var classrooms = await _classroomAppService.GetAllClassroomAsync();

            if (classrooms.Count.Equals(0))
            {
                return NotFound("Ainda não existe alunos cadastrado no sistema");
            }

            return StatusCode(200, new { Response = classrooms });
        }

        /// <summary>
        ///     Cadastra uma turma nova no banco dados
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClassroomResponseViewModel), 201)]
        public async Task<IActionResult> SaveClassroom(ClassroomRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insire o número da turma");
            }

            if (await _classroomAppService.ClassroomExist(model.ClassroomNumber))
            {
                return BadRequest($"Turma {model.ClassroomNumber} já existe no banco!");
            }

            var result = await _classroomAppService.InsertClassroom(model.ClassroomNumber);

            if (!result)
            {
                return BadRequest("Ops! ocorreu um erro na insersão, estamos verificando o erro");
            }

            var classroomResponse = await _classroomAppService.GetClassroomByNumber(model.ClassroomNumber);

            return StatusCode(201, new { Response = classroomResponse });
        }


        /// <summary>
        ///     Atualiza um turma no banco de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateClassroom(ClassroomUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insire dados validos!");
            }

            if (!await _classroomAppService.ClassroomExist(model.OldNumber))
            {
                return NotFound($"Turma {model.OldNumber} não existe!");
            }

            var result = await _classroomAppService.UpdateClassroom(model);

            if (!result)
            {
                return BadRequest("Ops! ocorreu um erro na atualização, estamos verificando.....");
            }

            return StatusCode(200, new { Response = "Turma atualizada com sucesso!" });
        }

        /// <summary>
        ///     Exclui uma turma no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insire dados validos!!");
            }

            var quantityStudentInClassroom = await _classroomAppService.VerifyQuantityStudentInClassroom(id);

            if (quantityStudentInClassroom > 0)
            {
                return BadRequest($"Essa turma possue alunos, ALUNOS: {quantityStudentInClassroom}");
            }

            var result = await _classroomAppService.DeleteClassroom(id);

            if (!result)
            {
                return BadRequest("ops! ocorreu um erro na deleção, estamos resolvendo......");
            }

            return StatusCode(200, new {Response = "Turma excluida com sucesso!"});
        }
    }
}
