using Microsoft.EntityFrameworkCore;
using TestProject.BaseService.Dtos.StudentGroupsDto;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services;

public class StudentGroupService:IStudentGroupService
{
    private readonly IRepository<StudentGroup,Guid> _repository;

    public StudentGroupService(IRepository<StudentGroup, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreataStudentToGroupAsync(StudentGroupsCreateDto dto)
    {
        var exists = await _repository.GetAll().AnyAsync(x => x.GroupId == dto.GroupId && x.StudentId == dto.StudentId);

        if (exists)
            throw new Exception("Student already in group");

        var studentGroup = new StudentGroup
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId
        };

        await _repository.CreateAsync(studentGroup);
        return true;
    }

    public async Task<bool> DeleteStudentToGroup(Guid Id)
    {
        var GroupAndStudent = await _repository.GetByIdAsync(Id);
        if(GroupAndStudent is null)
            throw new Exception("Group Student mavjudemas");

        await _repository.DeleteAsync(Id);

        return true;    
    }
}
