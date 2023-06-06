using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Model;
using WebAPI.Persistence.Configuration;

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
            modelBuilder.Entity<Users>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Movies>()
                .HasKey(p => p.MovieId);

            // Update master data
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Movies> Movies { get; set; }
    }
}
