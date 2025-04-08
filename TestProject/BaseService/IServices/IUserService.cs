using TestProject.BaseService.Dtos.UserDto;

namespace TestProject.BaseService.IServices;

public interface IUserService
{
    Task<IEnumerable<UserResultDto>> GetAllAsync();
    Task<UserResultDto> GetByIdAsync(Guid Id);
    Task<bool> RegistrAsync(RegistrCreationDto dto);
    Task<UserResultDto> UpdateAsync(Guid Id, UserUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);
}
