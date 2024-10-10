using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Models;
using BusinessLayer.Logics;
using DAL.DTO;
namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IConfiguration configuration;
        ILogger logger;
        AuthenticationLogic Ballogic;
        public AuthenticationController(IConfiguration _configuration,
            ILogger<AuthenticationController> _logger,AuthenticationLogic ballogic)
        {
            configuration= _configuration;
            logger= _logger;
            Ballogic=  ballogic;
        }
        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel?.username) || string.IsNullOrEmpty(loginModel?.password))
                throw new InvalidOperationException("Username and Password can't be empty");

           var users= Ballogic.CanLogin(loginModel.username, loginModel.password);
            if (users == null)
                throw new InvalidOperationException("Invalid user");

           var jwt = GenerateJWTToken(users);
           logger.LogInformation("User loggin and JWT token generated for user "+ loginModel.username);
           return Ok(GenerateJWTToken(users));
        }

        private string GenerateJWTToken(UsersDto user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.FirstName),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2),
                issuer: configuration.GetValue<string>("JWTToken:Issuer"),
                audience: configuration.GetValue<string>("JWTToken:Audience"),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWTToken:JWT_Secret"))
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
