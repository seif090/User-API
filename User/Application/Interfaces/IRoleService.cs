using User.Application.DTOS;

namespace User.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> GetRoleByIdAsync(int id);
        Task<List<RoleDTO>> GetAllRolesAsync();
        Task<bool> CreateRoleAsync(CreatedRoleDTO roleDTO);
        Task<bool> UpdateRoleAsync(UpdateRoleDTO roleDTO);
        Task<bool> DeleteRoleAsync(int id);
    }
}
