using Microsoft.EntityFrameworkCore;
using TestProject.BaseService.Dtos.UserDto;
using TestProject.BaseService.Helpers;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Models.Entities.Roles;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services;

public class UserService : IUserService
{
    private readonly IRepository<User, Guid> _userRepository;
    private readonly IRepository<UserRole, int> _userRoleRepository;
    private readonly IAuthService _authService;

    public UserService(IRepository<User, Guid> userRepository,
                       IAuthService authService,
                       IRepository<UserRole, int> userRoleRepository)
    {
        _userRepository = userRepository;
        _authService = authService;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var user = await _userRepository.GetByIdAsync(Id);

        if (user is null)
            throw new Exception("user mavjud emas");

        await _userRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        var users = _userRepository.GetAll().Select(u=> new UserResultDto{
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            Email = u.Email,
        }).AsEnumerable();
        
        return users ?? throw new Exception("user mavjud emas");
    }

    public async Task<UserResultDto> GetByIdAsync(Guid Id)
    {
        var user = await _userRepository.GetByIdAsync(Id);

        if (user is null)
            throw new Exception("user mavjud emas");
        var userDto = new UserResultDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
        };
        return userDto; 
    }

    public async Task<bool> RegistrAsync(RegistrCreationDto dto)
    {
       var users = await _userRepository.GetAll().Where(u=>u.Email == dto.Email).FirstOrDefaultAsync();
       if(users is not null)
            throw new Exception("foydalanuvchi mavjud");

        var GeneretedPasswordHash = PasswordHelper.Hash(dto.Password);

        var userDto = new User
        {
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Email = dto.Email,
            UserName = dto.UserName,
            PasswordHash = GeneretedPasswordHash
        };

        var createUser = await _userRepository.CreateAsync(userDto);

        var userRole = new UserRole()
        {
            UserId = createUser.Id,
            RoleId = 1
        };

        await _userRoleRepository.CreateAsync(userRole);
        return true;
    }

    public async Task<UserResultDto> UpdateAsync(Guid Id, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(Id);

        if (user is null)
            throw new Exception("user mavjud emas");

        var userDto = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
        };
        var updateDto = await _userRepository.UpdateAsync(userDto);
        return new UserResultDto()
        {
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            UserName = updateDto.UserName,
            Email = updateDto.Email,
        };
    }
}
