using TestProject.BaseService.Dtos.StudentGroupsDto;

namespace TestProject.BaseService.IServices;

public interface IStudentGroupService
{
    public Task<bool> CreataStudentToGroupAsync(StudentGroupsCreateDto dto);
    public Task<bool> DeleteStudentToGroup(Guid Id);
}
