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
    public class DislikeReactionCommandHandler : ControllerBase, IRequestHandler<DislikeReactionCommand, MoviesDto>
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
            var movie = GetMovie() ?? throw new NotFoundException();
            var user = GetUser() ?? throw new NotFoundException();
            DislikeMovieExecutive();
            SaveChangeDislike();
            var result = _mapper.Map<Movies, MoviesDto>(movie);
            return await Task.FromResult(result);

            // Get movie with movie id.
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

            // Subtract like number.
            IActionResult DislikeMovieExecutive()
            {
                var userReaction = _movieVoteDbContext.Reactions.FirstOrDefault(x => x.MovieId.Equals(command.MovieId) && x.UserId.Equals(command.UserId));
                if (userReaction != null)
                {
                    if (userReaction.ReactionType == (int)ReactionType.Dislike)
                    {
                        throw new BadHttpRequestException("Already disliked!");
                    }
                    else
                    {
                        userReaction.ReactionType = (int)ReactionType.Dislike;
                        movie.Likes--;
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
                movie.Likes--;
                return Ok(movie);
            }

            // Save change like number.
            void SaveChangeDislike()
            {
                _movieVoteDbContext.SaveChanges();
            }
        }
    }
}
