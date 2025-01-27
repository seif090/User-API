using User.Core.Entities;

namespace User.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User.Core.Entities.User> GetUserByIdAsync(int id);
        Task<User.Core.Entities.User> GetUserByEmailAsync(string email);
        Task<List<User.Core.Entities.User>> GetAllUsersAsync();
        Task<bool> AddUserAsync(User.Core.Entities.User user);
        Task<bool> UpdateUserAsync(User.Core.Entities.User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
