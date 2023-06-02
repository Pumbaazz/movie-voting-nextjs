using AutoMapper;
using MediatR;
using WebAPI.Application.Mapper;
using WebAPI.Domain.DTO;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.Reactions
{
    public class DislikeReactionCommandHandler : IRequestHandler<DislikeReactionCommand, MoviesDto>
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
        public DislikeReactionCommandHandler(ApplicationDbContext movieVoteDbContext)
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
        public async Task<MoviesDto> Handle(DislikeReactionCommand command, CancellationToken cancellationToken)
        {
            var movie = GetMovie(command.MovieId) ?? throw new BadHttpRequestException("An error occurred. Please try again later.");
            DislikeMovieExecutive(movie);
            SaveChangeDislike();
            var result = _mapper.Map<Movies, MoviesDto>(movie);
            return await Task.FromResult(result);

            // Get movie with movie id.
            Movies? GetMovie(int movieId)
            {
                return _movieVoteDbContext.Movies.FirstOrDefault(x => x.MovieId == movieId);
            }

            // Subtract like number.
            void DislikeMovieExecutive(Movies movie)
            {
                if (movie.Likes > 0)
                {
                    movie.Likes--;
                }
            }

            // Save change like number.
            void SaveChangeDislike()
            {
                _movieVoteDbContext.SaveChanges();
            }
        }
    }
}
