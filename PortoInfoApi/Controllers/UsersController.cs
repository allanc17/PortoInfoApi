// Local: /Controllers/UsersController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortoInfoApi.Models;
using PortoInfoApi.Services;

namespace PortoInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService; // Injeção do UserService

        // O Controller injeta o Service
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // 1. Endpoint Público: Registro (Criação de Usuário)
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            try
            {
                // Controller delega a lógica para o Service
                var newUser = await _userService.RegisterAsync(user);
                return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, newUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request
            }
        }

        // 2. Endpoint Público: Login (Geração de Token)
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] User loginInfo)
        {
            // Controller delega a autenticação para o Service
            var token = await _userService.AuthenticateAsync(loginInfo.Username, loginInfo.PasswordHash);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." }); // 401 Unauthorized
            }

            return Ok(new { token = token }); // 200 OK com o JWT
        }

        // 3. Endpoint Protegido: Obter Todos os Usuários
        [HttpGet]
        [Authorize(Roles = "Admin")] // Requer um token JWT VÁLIDO E com a permissão (Role) "Admin"
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // O pipeline de segurança (JWT) já validou o usuário. Agora busca os dados.
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}