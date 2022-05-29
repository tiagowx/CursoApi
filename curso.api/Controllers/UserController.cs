using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using curso.api.Models.Errors;
using curso.api.Models.Users;
using curso.api.Models.Validations;
using curso.api.Models.Filters;

namespace curso.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [
            SwaggerResponse(
                statusCode:
                200,
                description:
                "Sucesso ao autenticar",
                Type = typeof (LoginViewModelInput))
        ]
        [
            SwaggerResponse(
                statusCode:
                400,
                description:
                "Sucesso ao autenticar",
                Type = typeof (ValidateFieldViewModelOutput))
        ]
        [
            SwaggerResponse(
                statusCode:
                500,
                description:
                "Sucesso ao autenticar",
                Type = typeof (ErrorGenericViewModel))
        ]
        [HttpPost]
        [Route("login")]
        [ValidationModelStateCustom]
        public IActionResult SignIn(LoginViewModelInput loginViewModel)
        {
            return Ok(loginViewModel);
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(SignUpViewModelInput signUpViewModel)
        {
            return Created("", signUpViewModel);
        }
    }
}
