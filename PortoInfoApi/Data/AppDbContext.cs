using Microsoft.EntityFrameworkCore;
using PortoInfoApi.Models; 

namespace PortoInfoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = default!;
    }
}