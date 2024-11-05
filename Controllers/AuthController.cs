using Microsoft.AspNetCore.Mvc;
using workHome.Services;

namespace workHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _tokenService;
        public AuthController(JwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password, string userRole)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Preencha os campos");
            }

                var token = _tokenService.GenerateJwtToken(username, userRole);
                return Ok(new { Token = token });
        

        }
    }
}
