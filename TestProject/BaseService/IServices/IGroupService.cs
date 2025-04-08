using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.Dtos.UserDto;

namespace TestProject.BaseService.IServices;

public interface IGroupService
{
    Task<IEnumerable<GroupResultDto>> GetAllAsync();
    Task<GroupResultDtos> GetByIdAsync(Guid Id);
    Task<bool> CreateAsync(GroupCreateDto dto);
    Task<bool> UpdateAsync(Guid Id, GroupUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
