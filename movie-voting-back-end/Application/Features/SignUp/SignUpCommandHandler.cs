using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
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
            var user = GetUser(command.Email);
            if (user != null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            var newUser = new Users { Email = command.Email, Name = command.Name, Password = command.Password };
            SaveNewUser(newUser);
            // return StatusCode(StatusCodes.Status201Created);
            return Task.FromResult<IActionResult>(Created("", newUser));
        }

        // Get user by email.
        Users? GetUser(string email)
        {
            return _movieVoteDbContext.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        // Handle save new user.
        void SaveNewUser(Users newUser)
        {
            _movieVoteDbContext.Users.Add(newUser);
            _movieVoteDbContext.SaveChanges();
        }
    }
}
