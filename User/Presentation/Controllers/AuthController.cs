using Microsoft.AspNetCore.Mvc;
using User.Application.DTOS;
using User.Application.Services;
using User.Core.Interfaces;
using User.Infrastructure.Services;


namespace User.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService userService;
        private readonly TokenService _tokenService;

        public AuthController(IUserRepository userRepository, TokenService tokenService, IUserService userService)
        {
            _userRepository = userRepository;
            this.userService = userService;
            

            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var user = await userService.RegisterAsync(registerDTO);
                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
