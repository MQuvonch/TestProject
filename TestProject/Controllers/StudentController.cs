using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.BaseService.Dtos.StudentDto;
using TestProject.BaseService.IServices;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StudentCreateDto dto)
        {
            var response = await _studentService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(Guid Id, [FromBody] StudentUpdateDto dto)
        {
            var response = await _studentService.UpdateAsync(Id, dto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _studentService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetIdAsync(Guid Id)
        {
            var response = await _studentService.GetByIdAsync(Id);
            return Ok(response);
        }
    }
}
