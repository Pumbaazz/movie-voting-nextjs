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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Movies>()
                .HasKey(p => p.MovieId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movies> Movies { get; set; }
    }
}
