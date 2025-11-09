// Local: /Interfaces/IUserRepository.cs
using PortoInfoApi.Models;
using System.Threading.Tasks;

namespace PortoInfoApi.Interfaces
{
    // A interface define o contrato: o que o repositório deve ser capaz de fazer.
    public interface IUserRepository
    {
        // Todos os métodos de acesso a dados (I/O) são assíncronos (Task).
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
    }
}