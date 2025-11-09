// Local: /Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PortoInfoApi.Models; // Importa a classe User

namespace PortoInfoApi.Data
{
    // O AppDbContext herda de DbContext e gerencia a conexão e as tabelas.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Isso cria a tabela "Users" no nosso banco de dados em memória.
        public DbSet<User> Users { get; set; } = default!;
    }
}