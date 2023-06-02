using MediatR;
using WebAPI.Domain.DTO;

namespace WebAPI.Application.Features.Reactions
{
    public class DislikeReactionCommand : IRequest<MoviesDto>
    {
        /// <summary>
        /// Gets or sets the movie id.
        /// </summary>
        public int MovieId { get; set; }
    }
}
