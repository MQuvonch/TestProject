using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.UserDto;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Enums;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrCreationDto dto)
        {
            var response = await _userService.RegistrAsync(dto);
            return Ok(response);
        }


        
    }
}
