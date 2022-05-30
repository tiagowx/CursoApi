using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using curso.api.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace curso.api.Configurations
{
    public class JwtService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object GetToken(UserViewModelOutput userViewModelOutput)
        {
            var secret =
                Encoding
                    .ASCII
                    .GetBytes(_configuration
                        .GetSection("JwtConfiguration:Secret")
                        .Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor =
                new SecurityTokenDescriptor {
                    Subject =
                        new ClaimsIdentity(new Claim[] {
                                new Claim(ClaimTypes.NameIdentifier,
                                    userViewModelOutput.Id.ToString()),
                                new Claim(ClaimTypes.NameIdentifier,
                                    userViewModelOutput.Name.ToString()),
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

            return token;
        }
    }
}
