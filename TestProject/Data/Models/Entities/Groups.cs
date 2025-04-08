using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestProject.Data.Models.Commons;

namespace TestProject.Data.Models.Entities;

public class Groups : Auditable<Guid>
{
    public string Name { get; set; }    
    public double Price {  get; set; }
    public Guid MentorId { get; set; }
    [ForeignKey(nameof(MentorId))]
    public User Mentor { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
}
