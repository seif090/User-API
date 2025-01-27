using System.Text.Json;
using User.Core.Entities;
using User.Infrastructure.Data;

namespace User.Infrastructure.DataSeeding
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                var rolesJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "roles.json"));
                var roles = JsonSerializer.Deserialize<List<Role>>(rolesJson);

                if (roles != null)
                {
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }
            }
            if (!context.Users.Any())
            {
                var usersJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "users.json"));
                var users = JsonSerializer.Deserialize<List<User.Core.Entities.User>>(usersJson);

                if (users != null)
                {
                    // Hash passwords before saving users
                    foreach (var user in users)
                    {
                        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                    }

                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
            }
        }
    }
}
