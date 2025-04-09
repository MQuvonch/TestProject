namespace TestProject.BaseService.Dtos.RoleDto;

public class RoleResultDto
{
    public int Id { get; set; }
    public string Name {  get; set; }   
    public List<UserDto> Users { get; set; }    
}

public class UserDto
{
    public Guid Id { get; set; }    
    public string FullName {  get; set; }   
    public string Email { get; set; }   
}
