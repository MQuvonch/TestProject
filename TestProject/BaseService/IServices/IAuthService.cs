using TestProject.BaseService.Dtos.AuthDto;

namespace TestProject.BaseService.IServices;

public interface IAuthService
{
    Task<LoginForResultDto> AuthenticateAsync(LoginDto loginDto);
    Guid TokenFromUserId();
}
