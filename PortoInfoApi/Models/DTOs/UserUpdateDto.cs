using System.ComponentModel.DataAnnotations;

namespace PortoInfoApi.Models.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public required int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
    }
}