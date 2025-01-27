using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOS;
using User.Application.Interfaces;

namespace User.Presentation.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreatedRoleDTO roleDTO)
        {
            var result = await _roleService.CreateRoleAsync(roleDTO);
            return result ? Ok(new { Message = "Role created successfully." }) : BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO roleDTO)
        {
            var result = await _roleService.UpdateRoleAsync(roleDTO);
            return result ? Ok(new { Message = "Role updated successfully." }) : BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return result ? Ok(new { Message = "Role deleted successfully." }) : BadRequest();
        }
    }
}
