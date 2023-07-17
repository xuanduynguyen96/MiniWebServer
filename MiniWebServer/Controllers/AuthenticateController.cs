using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MiniWebServer.Models;
using MiniWebServer.Services.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public AuthenticateController(IJwtAuthenticationService jwtAuthenticationService)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
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
                Dictionary<string, string> claims = new()
                {
                    { System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, creadential.Username }
                };

                return Ok(_jwtAuthenticationService?.GetToken(claims));
            }
            return Unauthorized();
        }
    }
}
