using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Features.GetAllMovies;
using WebAPI.Application.Features.Reactions;
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
        /// <param name="mediator">The mediator.</param>
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
        //[ResponseCache(Duration = 10)]
        public async Task<IEnumerable<Movies>> GetAllMovies()
        {
            var result = await _mediator.Send(new GetAllMoviesQuery()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Update like number when action is like.
        /// </summary>
        /// <param name="command">The request command.</param>
        /// <returns>Movie modified.</returns>
        [HttpPatch]
        [Route("like")]
        public async Task<IActionResult> UpdateReactionLike([FromBody] LikeReactionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Update like number when action is dislike.
        /// </summary>
        /// <param name="command">The request command.</param>
        /// <returns>Movie modified.</returns>
        [HttpPatch]
        [Route("dislike")]
        public async Task<IActionResult> UpdateReactionDislike([FromBody] DislikeReactionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result;
        }
    }
}
