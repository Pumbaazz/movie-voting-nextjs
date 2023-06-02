namespace WebAPI.Domain.DTO
{
    public class MoviesDto
    {
        /// <summary>
        /// Gets or sets the movie ID.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the movie thumbnail path.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user react to movie.
        /// </summary>
        public int Likes { get; set; }
    }
}
