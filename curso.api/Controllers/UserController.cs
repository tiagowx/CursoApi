using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Infra.Data;
using curso.api.Models.Errors;
using curso.api.Models.Filters;
using curso.api.Models.Users;
using curso.api.Models.Validations;

namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IAuthenticationService _authenticationService;

        public UserController(
            IUserRepository userRepository,
            IConfiguration configuration,
            IAuthenticationService authenticationService
        )
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

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
            var user = _userRepository.GetUser(loginViewModel.Login);

            if(user == null)
            {
                return  BadRequest("Erro ao tentar acessar.");
            }

            var userViewModelOutput =
                new UserViewModelOutput()
                { 
                    Id = user.Id, 
                    Name = user.Name, 
                    Email = user.Email};

            var token = _authenticationService.GetToken(userViewModelOutput);

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

            // var pendingMigrations = context.Database.GetPendingMigrations();
            // if (pendingMigrations.Count() > 0)
            // {
            //     context.Database.Migrate();
            // }
            var user = new User();
            user.Name = signUpViewModelInput.Name;
            user.Email = signUpViewModelInput.Email;
            user.Password = signUpViewModelInput.Pasword;

            _userRepository.Add (user);
            _userRepository.Commit();

            return Created("", signUpViewModelInput);
        }
    }
}
