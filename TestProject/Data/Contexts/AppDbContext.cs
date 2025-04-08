using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TestProject.Data.Models.Entities;
using TestProject.Data.Models.Entities.Roles;

namespace TestProject.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }  
    public DbSet<Groups> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }
    public DbSet<Role> Roles { get; set; }  
    public DbSet<UserRole> UsersRoles { get; set; } 
  
}
