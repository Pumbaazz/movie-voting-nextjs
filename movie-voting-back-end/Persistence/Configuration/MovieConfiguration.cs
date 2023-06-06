using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Model;

namespace WebAPI.Persistence.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movies>
    {
        public void Configure(EntityTypeBuilder<Movies> builder)
        {
            builder.ToTable("Movies");
            builder.HasData(new List<Movies>
            {
                new Movies{ MovieId = 1, Title = "The Shawshank Redemption", Likes = 10, Path = "https://traditiononline.org/wp-content/uploads/2019/11/13-Best-Shawshank.jpg"},
                new Movies{ MovieId = 2, Title = "The Godfather", Likes = 12, Path = "https://www.lab111.nl/wp-content/uploads/2022/01/TGF50_INTL_DIGITAL_PAYOFF_1_SHEET__NED.jpg"},
                new Movies{ MovieId = 3, Title = "The Dark Knight", Likes = 5, Path = "https://m.media-amazon.com/images/I/91KkWf50SoL._AC_SL1500_.jpg"},
            });
        }
    }
}
