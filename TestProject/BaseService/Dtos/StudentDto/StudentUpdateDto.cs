using TestProject.Data.Models.Entities;

namespace TestProject.BaseService.Dtos.StudentDto
{
    public class StudentUpdateDto
    {
        public string FullName { get; set; }
        public double PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}
