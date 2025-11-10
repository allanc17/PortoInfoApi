using System.ComponentModel.DataAnnotations;

namespace PortoInfoApi.Models.DTOs
{
    public class UserDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}