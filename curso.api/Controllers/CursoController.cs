using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using curso.api.Models.Cursos;
using Microsoft.AspNetCore.Authorization;

namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : Controller
    {
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
            // var UserId =
            //     int
            //         .Parse(User
            //             .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)
            //             .Value);
            var cursos = new List<CursoViewModelOutput>();
            cursos
                .Add(new CursoViewModelOutput()
                {
                    Login = "()",
                    Description = "teste",
                    Name = "test"
                });

            return Ok(cursos);
        }
    }
}
