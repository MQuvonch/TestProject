using TestProject.Data.Models.Entities;

namespace TestProject.BaseService.Dtos.StudentDto
{
    public class StudentCreateDto
    {
        public string FullName { get; set; }
        public double PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }

}
