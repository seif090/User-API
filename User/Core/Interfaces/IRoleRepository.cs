using User.Core.Entities;

namespace User.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(int id);
        Task<Role> GetRoleByNameAsync(string name);
        Task<List<Role>> GetAllRolesAsync();
        Task<bool> AddRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
    }
}
