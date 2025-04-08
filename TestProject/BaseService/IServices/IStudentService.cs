using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.Dtos.StudentDto;

namespace TestProject.BaseService.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResultDto>> GetAllAsync();
        Task<StudentResultDto> GetByIdAsync(Guid Id);
        Task<bool> CreateAsync(StudentCreateDto dto);
        Task<bool> UpdateAsync(Guid Id, StudentUpdateDto dto);
        Task<bool> DeleteAsync(Guid Id);
    }
}
