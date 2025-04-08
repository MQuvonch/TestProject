using TestProject.Data.Models.Commons;

namespace TestProject.Data.Models.Entities;

public class Student : Auditable<Guid>
{
    public string FullName {  get; set; }
    public double PhoneNumber {  get; set; }
    public DateTime DateOfBirth {  get; set; }
    public string Gender { get; set; } 
    public ICollection<StudentGroup> Groups { get; set; }   
}
