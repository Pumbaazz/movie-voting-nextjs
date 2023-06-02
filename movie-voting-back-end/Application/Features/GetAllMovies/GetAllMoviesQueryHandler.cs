using MediatR;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.GetAllMovies
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movies>>
    {
        /// <summary>
        /// Application db context.
        /// </summary>
        public ApplicationDbContext MovieVoteDbContext;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="movieVoteDbContext">The movie vote db context.</param>
        public GetAllMoviesQueryHandler(ApplicationDbContext movieVoteDbContext)
        {
            MovieVoteDbContext = movieVoteDbContext;
        }

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<Movies>> Handle(GetAllMoviesQuery query, CancellationToken cancellationToken)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return await Task.FromResult(GetAllMovies());

            // Get all movies.
            IEnumerable<Movies> GetAllMovies()
            {
                return MovieVoteDbContext.Movies.OrderBy(x => x.MovieId).ToList();
            }
        }
    }
}
