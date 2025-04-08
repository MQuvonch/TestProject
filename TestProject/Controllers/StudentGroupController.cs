using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.StudentGroupsDto;
using TestProject.BaseService.IServices;
using TestProject.BaseService.Services;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private readonly IStudentGroupService _studentGroupService;

        public StudentGroupController(IStudentGroupService studentGroupService)
        {
            _studentGroupService = studentGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentGroupsCreateDto dto)
        {
            var response = await _studentGroupService.CreataStudentToGroupAsync(dto);
            return Ok(response);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var response = await _studentGroupService.DeleteStudentToGroup(Id);
            return Ok(response);
        }
    }
}
