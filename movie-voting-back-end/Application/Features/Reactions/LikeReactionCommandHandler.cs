using AutoMapper;
using MediatR;
using WebAPI.Application.Mapper;
using WebAPI.Domain.DTO;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.Reactions
{
    public class LikeReactionCommandHandler : IRequestHandler<LikeReactionCommand, MoviesDto>
    {
        /// <summary>
        /// Application db context.
        /// </summary>
        private readonly ApplicationDbContext _movieVoteDbContext;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="movieVoteDbContext">The movie vote db context.</param>
        public LikeReactionCommandHandler(ApplicationDbContext movieVoteDbContext)
        {
            var config = new MapperConfiguration(MovieMapper.CreateMap);

            _mapper = config.CreateMapper();
            _movieVoteDbContext = movieVoteDbContext;
        }

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<MoviesDto> Handle(LikeReactionCommand command, CancellationToken cancellationToken)
        {
            var movie = GetMovie(command.MovieId) ?? throw new BadHttpRequestException("An error occurred. Please try again later.");
            LikeMovieExecutive(movie);
            SaveChangeLike();
            var result = _mapper.Map<Movies, MoviesDto>(movie);
            return await Task.FromResult(result);

            // Get movie with movie id.
            Movies? GetMovie(int movieId)
            {
                return _movieVoteDbContext.Movies.FirstOrDefault(x => x.MovieId == movieId);
            }

            // Count up like number.
            void LikeMovieExecutive(Movies movie)
            {
                movie.Likes++;
            }

            // Save change like number.
            void SaveChangeLike()
            {
                _movieVoteDbContext.SaveChanges();
            }
        }
    }
}
