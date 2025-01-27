using User.Application.DTOS;

namespace User.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(CreateUserDTO userDTO);
        Task<bool> UpdateUserAsync(UpdatedUserDTO userDTO);
        Task<bool> DeleteUserAsync(int id);
        Task<Entities.User> RegisterAsync(RegisterDTO registerDTO);
    }
}
