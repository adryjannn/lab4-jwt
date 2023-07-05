using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentProject.Models;

public class StudentContext : IdentityDbContext<UserEntity,
    UserRole, int>
{
    public StudentContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"Data Source=student.db");
        options.EnableSensitiveDataLogging();
    }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StudentContext(
                   serviceProvider.GetRequiredService<DbContextOptions<StudentContext>>()))
        {
            context.Database.EnsureCreated();
        }
    }
}