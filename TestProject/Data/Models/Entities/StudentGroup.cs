using System.ComponentModel.DataAnnotations.Schema;
using TestProject.Data.Models.Commons;

namespace TestProject.Data.Models.Entities;

public class StudentGroup : Auditable<Guid>
{
    public Guid StudentId { get; set; }
    [ForeignKey(nameof(StudentId))] 
    public Student Student {  get; set; }   

    public Guid GroupId { get; set; }  
    [ForeignKey(nameof(GroupId))]
    public Groups Group { get; set; }    
}
