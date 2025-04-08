using TestProject.Data.Models.Entities;

namespace TestProject.BaseService.Dtos.StudentDto
{
    public class StudentResultDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public List<GroupDto> Groups { get; set; }
    }

    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Mentor { get; set; }
    }


}
