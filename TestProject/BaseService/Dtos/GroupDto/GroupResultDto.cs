using TestProject.BaseService.Dtos.GroupDto;

namespace TestProject.BaseService.Dtos.GroupDto
{
    public class GroupResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Mentor { get; set; }
        public int Count { get; set; }
        public List<StudentDto> Students { get; set; }
    }
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}

public class GroupResultDtos
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Mentor { get; set; }
    public List<StudentDto> Students { get; set; }
}



