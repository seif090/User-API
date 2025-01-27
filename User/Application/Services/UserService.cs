
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using User.Application.DTOS;
using User.Core.Exceptions;
using User.Core.Interfaces;
using User.Infrastructure.Services;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
        }

        // Register a new user
        public async Task<User.Core.Entities.User> RegisterAsync(RegisterDTO registerDTO)
        {
            // Check if the email is already registered
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }

            // Assign the default role (e.g., "User")
            var defaultRole = await _roleRepository.GetRoleByNameAsync("User");
            if (defaultRole == null)
            {
                throw new Exception("Default role not found.");
            }

            // Create the user
            var user = new User.Core.Entities.User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = defaultRole.Id,
                Role = defaultRole // Ensure the Role is set
            };

            // Save the user
            var result = await _userRepository.AddUserAsync(user);
            if (!result)
            {
                throw new Exception("User registration failed.");
            }

            // Return the user object
            return user;
        }
        // Authenticate a user
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            // Find the user by email
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            // Generate a JWT token
            var token = _tokenService.GenerateToken(user);
            return token;
        }

        // Get user by ID
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                RoleName = user.Role.Name
            };
        }

        // Get all users
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                RoleName = user.Role.Name
            }).ToList();
        }

        // Update user profile
        public async Task<bool> UpdateUserAsync(UpdatedUserDTO userDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(int.Parse(userDTO.Id));
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var role = await _roleRepository.GetRoleByIdAsync(userDTO.RoleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.RoleId = userDTO.RoleId;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepository.UpdateUserAsync(user);
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> CreateUserAsync(CreateUserDTO userDTO)
        {
            var role = await _roleRepository.GetRoleByIdAsync(userDTO.RoleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            var user = new User.Core.Entities.User 
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = userDTO.RoleId
            };

            return await _userRepository.AddUserAsync(user);

        }
    }
}