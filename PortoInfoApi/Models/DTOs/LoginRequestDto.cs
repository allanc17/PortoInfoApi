using System.ComponentModel.DataAnnotations;

namespace PortoInfoApi.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string PasswordHash { get; set; }
    }
}