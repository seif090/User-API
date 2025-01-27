namespace User.Application.Factories
{
    public static class UserFactory
    {
        public static User.Core.Entities.User CreateUser(string firstName, string lastName, string email, string password)
        {
            return new User.Core.Entities.User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
