using System.Net;
using System.Web.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.SignUp
{
    public class SignUpCommandHandler : ControllerBase, IRequestHandler<SignUpCommand, IActionResult>
    {
        /// <summary>
        /// Application db context.
        /// </summary>
        private readonly ApplicationDbContext _movieVoteDbContext;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="movieVoteDbContext">The movie vote db context.</param>
        public SignUpCommandHandler(ApplicationDbContext movieVoteDbContext)
        {
            _movieVoteDbContext = movieVoteDbContext;
        }

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task<IActionResult> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var user = GetUser();
            if (user != null)
            {
                return Task.FromResult<IActionResult>(Conflict());
            }

            var newUser = new Users
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                Name = command.Name,
                Password = command.Password
            };

            SaveNewUser();
            return Task.FromResult<IActionResult>(Created("", newUser));

            // Get user by email.
            Users? GetUser()
            {
                return _movieVoteDbContext.Users.FirstOrDefault(x => x.Email.Equals(command.Email));
            }

            // Handle save new user.
            void SaveNewUser()
            {
                _movieVoteDbContext.Users.Add(newUser);
                _movieVoteDbContext.SaveChanges();
            }
        }
    }
}
