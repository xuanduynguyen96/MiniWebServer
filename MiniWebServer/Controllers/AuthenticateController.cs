using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MiniWebServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("verifytoken")]
        public IActionResult VerifyToken()
        {
            return Ok("The token is still valid");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createtoken")]
        public IActionResult CreateToken(User creadential)
        {
            // TODO: Below logic uses a dummy user to create token, consider using a realistic data when running on Production environment
            if ("testuser".Equals(creadential.Username) && "testpass".Equals(creadential.Password))
            {
                // TODO: Once again, these configurations below gotten from json file so consider to replase this code with Options pattern's code, pls looking forward to future commit to get things updated
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!); // NOTE: The passed secret key is marked with '!' to make sure that it's not null

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, creadential.Username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5), // TODO: This expires time is hard code which is not good, make it able to change in future by storing it in a config file
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return Ok(jwtToken);
            }
            return Unauthorized();
        }
    }
}
