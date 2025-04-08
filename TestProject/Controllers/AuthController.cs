using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.AuthDto;
using TestProject.BaseService.IServices;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var token = await _authService.AuthenticateAsync(dto);
            return Ok(token);
        }
    }
}
