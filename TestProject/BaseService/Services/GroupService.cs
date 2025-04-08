using Microsoft.EntityFrameworkCore;
using TestProject.BaseService.Dtos.GroupDto;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services;

public class GroupService : IGroupService
{
    private readonly IRepository<Groups, Guid> _groupRepository;
    private readonly IAuthService _authService;

    public GroupService(IRepository<Groups, Guid> groupRepository,
                        IAuthService authService)
    {
        _groupRepository = groupRepository;
        _authService = authService;
    }

    public async Task<bool> CreateAsync(GroupCreateDto dto)
    {
        var group = await _groupRepository.GetAll().Where(g => g.Name == dto.Name).FirstOrDefaultAsync();
        if (group is not null)
            throw new Exception($"bu {dto.Name} guruh mavjud");

        var groupdto = new Groups()
        {
            Name = dto.Name,
            Price = dto.Price,
            MentorId = _authService.TokenFromUserId(),
        };

        var creataGroup = await _groupRepository.CreateAsync(groupdto);

        return true;
    }

    public Task<bool> DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GroupResultDto>> GetAllAsync()
    {
        var groups = _groupRepository.GetAll().Select(g => new GroupResultDto()
        {
            Id = g.Id,
            Name = g.Name,
            Price = g.Price,
            Mentor = g.Mentor.FirstName + " " + g.Mentor.LastName,
            Count = g.StudentGroups.Count()
        }).AsEnumerable();

        return groups;
    }


    public async Task<GroupResultDtos> GetByIdAsync(Guid Id)
    {
        var result = await _groupRepository.GetByIdAsync(Id);
        if (result is null)
            throw new Exception("bu group mavjud emas");

        var group = await _groupRepository.GetAll()
            .Where(g => g.Id == Id)
            .Include(g => g.StudentGroups)
                .ThenInclude(sg => sg.Student)
            .Select(g => new GroupResultDtos
            {
                Id = g.Id,
                Name = g.Name,
                Price = g.Price,
                Mentor = g.Mentor.FirstName + " " + g.Mentor.LastName,
                Students = g.StudentGroups.Select(sg => new StudentDto
                {
                    Id = sg.Student.Id,
                    FullName = sg.Student.FullName,
                    PhoneNumber = sg.Student.PhoneNumber,
                    DateOfBirth = sg.Student.DateOfBirth,
                    CreatedAt = sg.CreatedAt

                }).ToList()
            })
            .FirstOrDefaultAsync();

        

        return group;
    }


    public async Task<bool> UpdateAsync(Guid Id, GroupUpdateDto dto)
    {
        var group = await _groupRepository.GetByIdAsync(Id);
        if (group is null)
            throw new Exception("Group mavjud emas");

        group.Price = dto.Price;
        group.Name = dto.Name;

        await _groupRepository.UpdateAsync(group);
        return true;
    }
}
