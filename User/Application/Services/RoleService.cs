using User.Application.DTOS;
using User.Application.Interfaces;
using User.Core.Entities;
using User.Core.Exceptions;
using User.Core.Interfaces;

namespace User.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDTO> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return roles.Select(role => new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }
        public async Task<bool> CreateRoleAsync(CreatedRoleDTO roleDTO)
        {
            var role = new Role
            {
                Name = roleDTO.Name
            };

            return await _roleRepository.AddRoleAsync(role);
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleDTO roleDTO)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleDTO.Id);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            role.Name = roleDTO.Name;
            return await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
