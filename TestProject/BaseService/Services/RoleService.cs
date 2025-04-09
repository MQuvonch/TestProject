using Microsoft.EntityFrameworkCore;
using TestProject.BaseService.Dtos.RoleDto;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Models.Entities.Roles;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services;

public class RoleService : IRoleService
{
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IRepository<UserRole, int> _userRoleRepository;
    private readonly IRepository<User, Guid> _userRepository;

    public RoleService(IRepository<Role, int> roleRepository,
                       IRepository<UserRole, int> userRoleRepository,
                       IRepository<User, Guid> userRepository)
    {
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> AssignRoleAsync(AssignRoleDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(dto.RoleId);
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (role is null || user is null)
            throw new Exception("User yoki role topilmadi");

        var userRolDto = new UserRole()
        {
            RoleId = role.Id,
            UserId = user.Id,
        };
        await _userRoleRepository.CreateAsync(userRolDto);

        return true;
    }

    public async Task<bool> CreateRoleAsync(RoleCreateDto dto)
    {
        var roles = await _roleRepository.GetAll()
            .Where(r=>r.Name == dto.Name).FirstOrDefaultAsync();
        if (roles is not null)
            throw new Exception("Role Mavjud");

        var roleDto = new Role()
        {
            Name = dto.Name,
        };

        await _roleRepository.CreateAsync(roleDto);
        return true;    
    }

    public async Task<bool> DeleteRoleAsync(int Id)
    {
        var result = await _roleRepository.GetByIdAsync(Id);
        if (result is null)
            throw new Exception("Role topilmadi");

        await _roleRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<RoleResultDto>> GetAllAsync()
    {
        var roles = _roleRepository.GetAll().Select(r=>new RoleResultDto()
        {
            Id = r.Id,
            Name = r.Name,
            Users = r.Users.Select(u=>new UserDto()
            {
                Id=u.User.Id,    
                FullName = u.User.FirstName + " " + u.User.LastName,
                Email = u.User.Email,
            }).ToList()
        }).ToList();  
        
        return roles;
    }

    public async Task<RoleResultDto> GetByIdAsync(int id)
    {
        var role = _roleRepository.GetAll().Where(r => r.Id == id).FirstOrDefault();
        if (role is null)
            throw new Exception("Role mavjud emas");

        var userRole = _userRoleRepository.GetAll()
            .Where(r => r.RoleId == id)
            .Include(u => u.Role)
            .ThenInclude(s=>s.Users);

        var roleDto = new RoleResultDto()
        {
            Id = role.Id,
            Name = role.Name,
            Users = userRole.Select(sr=> new UserDto()
            {
                Id = sr.User.Id,
                FullName = sr.User.FirstName + " " + sr.User.LastName,
                Email= sr.User.Email,
            }).ToList(),
        };
        return roleDto;
    }

    public async Task<bool> UpdateRoleAsync(int Id, RoleUpdateDto dto)
    {
        var result = await _roleRepository.GetByIdAsync(Id);
        if (result is null)
            throw new Exception("Role topilmadi");

        result.Name = dto.Name;

        await _roleRepository.UpdateAsync(result);  
        return true;
    }
}
