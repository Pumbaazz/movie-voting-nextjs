using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Application.Features.SignUp
{
    public class SignUpCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
