using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.RoleDto;
using TestProject.BaseService.IServices;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(RoleCreateDto dto)
        {
            var response = await _roleService.CreateRoleAsync(dto);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserRoleAsync(AssignRoleDto dto)
        {
            var response = await _roleService.AssignRoleAsync(dto);
            return Ok(response);    
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _roleService.GetAllAsync();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _roleService.GetByIdAsync(id);
            return Ok(response);    
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(int id,RoleUpdateDto dto)
        {
            var response = await _roleService.UpdateRoleAsync(id, dto);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var response = await _roleService.DeleteRoleAsync(id);
            return Ok(response);
        }
    }
}
