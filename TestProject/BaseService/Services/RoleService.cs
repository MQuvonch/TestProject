using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.Dtos.UserDto;
using TestProject.BaseService.IServices;

namespace TestProject.BaseService.Services;

public class RoleService : IRoleService
{
    public Task<bool> CreateAsync(GroupCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GroupResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GroupResultDto> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<GroupResultDto> UpdateAsync(Guid Id, UserUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
