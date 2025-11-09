// Local: /Repository/UserRepository.cs
using Microsoft.EntityFrameworkCore;
using PortoInfoApi.Data;
using PortoInfoApi.Interfaces;
using PortoInfoApi.Models;

namespace PortoInfoApi.Repository
{
    // A implementação do Repositório usa o AppDbContext para falar com o banco.
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            // O AppDbContext é injetado pelo Program.cs
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // <-- Executa o INSERT no BD
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync(); // <-- Executa o SELECT no BD
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username); // <-- Executa o SELECT no BD
        }
    }
}