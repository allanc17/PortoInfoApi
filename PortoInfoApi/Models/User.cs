// Local: /Models/User.cs
namespace PortoInfoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // O hash da senha criptografada
        public string Role { get; set; } = "User"; // Define o nível de acesso (ex: "Admin")
    }
}