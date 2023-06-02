using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Features.GetAllMovies;
using WebAPI.Application.Features.Reactions;
using WebAPI.Domain.DTO;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="movieVoteDbContext">The movie db context.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The mapper.</param>
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all movies from database.
        /// </summary>
        /// <returns>List all movie.</returns>
        [HttpGet]
        [Route("get-movies")]
        public async Task<IEnumerable<Movies>> GetAllMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesQuery()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Update like number when action is like.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>Movie modified.</returns>
        [HttpPatch]
        [Route("like/{movieId}")]
        public async Task<MoviesDto> UpdateReactionLike(int movieId)
        {
            var result = await _mediator.Send(new LikeReactionCommand { MovieId = movieId }).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Update like number when action is dislike.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>Movie modified.</returns>
        [HttpPatch]
        [Route("dislike/{movieId}")]
        public async Task<MoviesDto> UpdateReactionDislike(int movieId)
        {
            var result = await _mediator.Send(new DislikeReactionCommand { MovieId = movieId }).ConfigureAwait(false);
            return result;
        }
    }
}
