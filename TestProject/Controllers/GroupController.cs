using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.IServices;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GroupCreateDto dto)
        {
            var response = await _groupService.CreateAsync(dto);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var response = await _groupService.GetByIdAsync(Id);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(Guid Id, GroupUpdateDto dto)
        {
            var response = await _groupService.UpdateAsync(Id, dto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _groupService.GetAllAsync();
            return Ok(response);
        }

    }
}
