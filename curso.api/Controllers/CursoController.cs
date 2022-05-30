using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Models.Cursos;

namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : Controller
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        //Post
        [
            SwaggerResponse(
                statusCode:
                201,
                description:
                "Cadastro realizado com sucesso!")
        ]
        [SwaggerResponse(statusCode: 401, description: "Não cadastrado!")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult>
        Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.Name = cursoViewModelInput.Name;
            curso.Description = cursoViewModelInput.Description;
            var userId =
                int
                    .Parse(User
                        .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)
                        .Value);

            curso.Id = userId;

            _cursoRepository.Add (curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }

        //Get
        [
            SwaggerResponse(
                statusCode:
                201,
                description:
                "Localizado com sucesso!")
        ]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado!")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var userId =
                int
                    .Parse(User
                        .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)
                        .Value);

            var cursos =
                _cursoRepository
                    .GetByUserId(userId)
                    .Select(s =>
                        new CursoViewModelOutput()
                        {
                            Name = s.Name,
                            Description = s.Description,
                            Login = s.User.Name
                        });

            return Ok(cursos);
        }
    }
}
