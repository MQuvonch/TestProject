using System.Data;
using TestProject.Data.Contexts;
using TestProject.Data.Models.Entities.Roles;

namespace TestProject.RoleSeedData;

public class SeedData
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if(!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role {Name = "Manager"},
                new Role {Name = "Teacher"}
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

    }
}
