using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Model;

namespace WebAPI.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasData(new Users
            {
                Id = 1,
                Name = "John Smith",
                Email = "admin@gmail.com",
                Password = "password",
            });
        }
    }
}
