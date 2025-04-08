using TestProject.Data.Models.Commons;

namespace TestProject.Data.Models.Entities.Roles
{
    public class Role : Auditable<int>
    {
        public string Name { get; set; }
    }
}
