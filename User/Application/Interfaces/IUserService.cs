using User.Application.DTOS;

namespace User.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(CreateUserDTO userDTO);
        Task<bool> UpdateUserAsync(UpdatedUserDTO userDTO);
        Task<bool> DeleteUserAsync(int id);
        Task <User.Core.Entities.User> RegisterAsync(RegisterDTO registerDTO);

    }
}
