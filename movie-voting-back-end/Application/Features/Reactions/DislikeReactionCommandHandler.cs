using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Behaviors;
using WebAPI.Application.Exceptions;
using WebAPI.Application.Mapper;
using WebAPI.Domain.DTO;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.Reactions
{
    public class DislikeReactionCommandHandler : ControllerBase, IRequestHandler<DislikeReactionCommand, IActionResult>
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
        public async Task<IActionResult> Handle(DislikeReactionCommand command, CancellationToken cancellationToken)
        {
            var movie = GetMovie() ?? throw new NotFoundException();
            var user = GetUser() ?? throw new NotFoundException();

            // Decrease like number.
            var userReaction = _movieVoteDbContext.Reactions.FirstOrDefault(x => x.MovieId.Equals(command.MovieId) && x.UserId.Equals(command.UserId));
            if (userReaction != null)
            {
                if (userReaction.ReactionType == (int)ReactionType.Dislike)
                {
                    return await Task.FromResult<IActionResult>(Conflict());
                }
                else
                {
                    userReaction.ReactionType = (int)ReactionType.Dislike;
                }
            }
            else
            {
                var reaction = new Domain.Model.Reactions
                {
                    Id = Guid.NewGuid(),
                    MovieId = command.MovieId,
                    UserId = command.UserId,
                    ReactionType = (int)ReactionType.Dislike
                };
                _movieVoteDbContext.Reactions.Add(reaction);
            }

            // Save changes.
            SaveChangeDislike();
            var result = _mapper.Map<Movies, MoviesDto>(movie);
            return Ok(result);

            // Get movie with movie id.
            Movies? GetMovie()
            {
                return _movieVoteDbContext.Movies.FirstOrDefault(x => x.Id.Equals(command.MovieId));
            }

            // Get user by token id.
            Users? GetUser()
            {
                return _movieVoteDbContext.Users.FirstOrDefault(x => x.Id.Equals(command.UserId));
            }

            // Save change like number.
            void SaveChangeDislike()
            {
                _movieVoteDbContext.SaveChanges();
            }
        }
    }
}
