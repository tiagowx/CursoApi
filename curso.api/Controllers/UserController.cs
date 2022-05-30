using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using curso.api.Business.Entities;
using curso.api.Infra.Data;
using curso.api.Models.Errors;
using curso.api.Models.Filters;
using curso.api.Models.Users;
using curso.api.Models.Validations;
using System.Linq;

namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
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
            var userViewModelOutput =
                new UserViewModelOutput()
                { Id = 1, Login = "Tiago", Email = "tiagowx@gmail.com" };

            var secret =
                Encoding.ASCII.GetBytes("chavealeatoriacommuitoscaracteres");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor =
                new SecurityTokenDescriptor {
                    Subject =
                        new ClaimsIdentity(new Claim[] {
                                new Claim(ClaimTypes.NameIdentifier,
                                    userViewModelOutput.Id.ToString()),
                                new Claim(ClaimTypes.NameIdentifier,
                                    userViewModelOutput.Login.ToString()),
                                new Claim(ClaimTypes.NameIdentifier,
                                    userViewModelOutput.Email.ToString())
                            }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials =
                        new SigningCredentials(symmetricSecurityKey,
                            SecurityAlgorithms.HmacSha256Signature)
                };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated =
                jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new { Token = token, User = userViewModelOutput });
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(SignUpViewModelInput signUpViewModelInput)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder
                .UseSqlServer("Server=127.0.0.1,1433;Database=Curso;User Id=SA;Password=mssql1Ipw");

            CursoDbContext context = new CursoDbContext(optionsBuilder.Options);

            var pendingMigrations = context.Database.GetPendingMigrations();
            if (pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new User();
            user.Name = signUpViewModelInput.Name;
            user.Email = signUpViewModelInput.Email;
            user.Password = signUpViewModelInput.Pasword;

            context.User.Add (user);
            context.SaveChanges();

            return Created("", signUpViewModelInput);
        }
    }
}
