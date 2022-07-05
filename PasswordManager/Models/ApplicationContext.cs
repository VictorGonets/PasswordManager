using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PasswordData> PasswordData { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }   
    }
}