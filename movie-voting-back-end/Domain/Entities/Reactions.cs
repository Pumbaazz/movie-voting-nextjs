using WebAPI.Application.Behaviors;

namespace WebAPI.Domain.Model
{
    public class Reactions
    {
        /// <summary>
        /// Gets or sets the movie ID.
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public Guid MovieId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the user react to movie.
        /// </summary>
        public ReactionType ReactionType { get; set; }
    }
}
