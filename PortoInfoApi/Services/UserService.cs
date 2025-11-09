// Local: /Services/UserService.cs

using PortoInfoApi.Interfaces;
using PortoInfoApi.Models;
using BCrypt.Net; // Usado para hash de senha
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt; // Usado para JWT
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PortoInfoApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        // O Service injeta o Repositório (Interface) e a Configuração (para pegar a chave JWT)
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            // Pega a chave secreta do appsettings.json
            _jwtSecret = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT não configurada.");
        }

        // --- Lógica de Registro (Assíncrona e Segura) ---
        public async Task<User> RegisterAsync(User user)
        {
            // Validação de Negócio: Usuário já existe?
            if (await _userRepository.GetByUsernameAsync(user.Username) != null)
            {
                throw new InvalidOperationException("Usuário já cadastrado.");
            }

            // SEGURANÇA: Cria o hash da senha (criptografia irreversível)
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Chama a camada de Repositório
            await _userRepository.AddAsync(user);
            return user;
        }

        // --- Lógica de Autenticação e Geração do Token JWT (Assíncrona) ---
        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            // 1. Verifica se o usuário existe E se a senha digitada corresponde ao hash salvo
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null; // Falha na autenticação
            }

            // 2. Se a autenticação OK, gera o Token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Claims são as informações do usuário que vão dentro do token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role) // Isso será usado no [Authorize(Roles="...")]
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Token expira em 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Retorna a string do token
        }

        // --- Lógica de Consulta (Assíncrona) ---
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}