using Microsoft.EntityFrameworkCore;

namespace web.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Diagram> Diagrams { get; set; }
    public DbSet<TelephonyAction> Actions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasData(
                new User { Id = 1, Username = "admin", IsAdmin = true }, 
                new User { Id = 2, Username = "user1", IsAdmin = false },
                new User { Id = 3, Username = "user2", IsAdmin = false }
                );
    }
}