using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Features.Login;
using WebAPI.Application.Features.SignUp;

namespace WebAPI.Application.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieVoteDbContext">MovieVoteDbContext.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login method.
        /// </summary>
        /// <returns>Data of user login successfully.</returns>
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="model">New user data payload.</param>
        /// <returns>Status code.</returns>
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return result;
        }
    }
}
