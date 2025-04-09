using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.Dtos.RoleDto;
using TestProject.BaseService.Dtos.UserDto;

namespace TestProject.BaseService.IServices;

public interface IRoleService
{
    public Task<bool> CreateRoleAsync(RoleCreateDto dto);
    public Task<bool> UpdateRoleAsync(int Id, RoleUpdateDto dto);
    public Task<RoleResultDto> GetByIdAsync(int Id);
    public Task<IEnumerable<RoleResultDto>> GetAllAsync();
    public Task<bool> DeleteRoleAsync(int Id);
    public Task<bool> AssignRoleAsync(AssignRoleDto dto);
}
