using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.DTO;

namespace WebAPI.Application.Features.Reactions
{
    public class LikeReactionCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the movie id.
        /// </summary>
        public Guid MovieId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
