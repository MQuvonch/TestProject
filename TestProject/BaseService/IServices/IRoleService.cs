using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.Dtos.UserDto;

namespace TestProject.BaseService.IServices;

public interface IRoleService
{
    Task<IEnumerable<GroupResultDto>> GetAllAsync();
    Task<GroupResultDto> GetByIdAsync(Guid Id);
    Task<bool> CreateAsync(GroupCreateDto dto);
    Task<GroupResultDto> UpdateAsync(Guid Id, UserUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
