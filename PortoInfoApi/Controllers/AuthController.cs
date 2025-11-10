using Microsoft.AspNetCore.Mvc;
using PortoInfoApi.Services;
using PortoInfoApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PortoInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.Username, loginDto.PasswordHash);

            if (user == null)
            {
                return Unauthorized(new { message = "Nome de usuário ou senha inválidos." });
            }

            var token = _userService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var newUser = await _userService.RegisterAsync(userDto.Username, userDto.PasswordHash, userDto.Role);

            if (newUser == null)
            {
                return BadRequest(new { message = "Falha ao registrar usuário (usuário já existe ou dados inválidos)." });
            }
            return Ok(newUser);
        }

        [HttpGet("isauthenticated")]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            return Ok(new { message = "Token válido. Usuário autenticado." });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout simbólico: o cliente deve descartar o token JWT." });
        }
    }
}