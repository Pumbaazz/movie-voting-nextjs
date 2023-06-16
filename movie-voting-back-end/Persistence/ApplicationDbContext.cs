using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Model;

namespace WebAPI.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Movies> Movies { get; set; }

        public DbSet<Reactions> Reactions { get; set; }
    }
}
