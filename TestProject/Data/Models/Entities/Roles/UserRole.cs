using System.ComponentModel.DataAnnotations.Schema;
using TestProject.Data.Models.Commons;

namespace TestProject.Data.Models.Entities.Roles;

public class UserRole : Auditable<int>
{
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }
}
