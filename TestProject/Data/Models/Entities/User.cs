using System.ComponentModel.DataAnnotations;
using TestProject.Data.Models.Commons;
using TestProject.Data.Models.Entities.Roles;

namespace TestProject.Data.Models.Entities;

    public class User : Auditable<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]

        public string UserName { get; set; }
        [Required]

        public string PasswordHash { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }


