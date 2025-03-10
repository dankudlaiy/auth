using Microsoft.EntityFrameworkCore;

namespace web.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AuthDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AuthDbContext>>());

        if (context?.Users == null)
        {
            throw new NullReferenceException(
                "Null BlazorWebAppMoviesContext or Users DbSet");
        }

        if (context.Users.Any())
        {
            return;
        }

        context.Users.AddRange(
            new User
            {
                Username = "admin",
                IsAdmin = true
            },
            new User
            {
                Username = "user1",
                IsAdmin = false
            },
            new User
            {
                Username = "user2",
                IsAdmin = false
            });

        context.SaveChanges();
    }
}