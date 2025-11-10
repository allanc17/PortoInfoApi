using Microsoft.AspNetCore.Mvc;
using PortoInfoApi.Models.DTOs;
using PortoInfoApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace PortoInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
            }
            return Ok(user);
        }

        [HttpPut("UpdateUserAsync/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest(new { message = "O ID na URL não corresponde ao ID no corpo da requisição." });
            }

            var success = await _userService.UpdateUserAsync(userDto);

            if (!success)
            {
                return NotFound(new { message = $"Falha ao atualizar. Usuário com ID {id} não encontrado ou dados inválidos." });
            }

            return NoContent();
        }
    }
}