using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Application.Features.Login
{
    public class LoginCommand : IRequest<IActionResult>
    {
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
