using JensenOnline.Api.Models;

namespace JensenOnline.Api.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin"
            });
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Laptop", Price = 9999, Description = "Modern laptop" },
                new Product { Name = "Keyboard", Price = 799, Description = "Mechanical keyboard" },
                new Product { Name = "Mouse", Price = 499, Description = "Wireless mouse" }
            );
        }

        context.SaveChanges();
    }
}
