using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Domain.Model;
using WebAPI.Persistence;

namespace WebAPI.Application.Features.Login
{
    public class LoginCommandHandler : ControllerBase, IRequestHandler<LoginCommand, IActionResult>
    {
        /// <summary>
        /// Application db context.
        /// </summary>
        private readonly ApplicationDbContext _movieVoteDbContext;

        /// <summary>
        /// Configuration.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="movieVoteDbContext">The movie vote db context.</param>
        /// <param name="config">The config.</param>
        public LoginCommandHandler(ApplicationDbContext movieVoteDbContext, IConfiguration config)
        {
            _movieVoteDbContext = movieVoteDbContext;
            _config = config;
        }

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task<IActionResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = GetUser(command.Email, command.Password);
            if (user == null)
            {
                return Task.FromResult<IActionResult>(Unauthorized());
            }

            return Task.FromResult<IActionResult>(Ok(new { token = new JwtSecurityTokenHandler().WriteToken(CreateJwtToken()) }));

            // Get user.
            User? GetUser(string userEmail, string userPassword)
            {
                return _movieVoteDbContext.Users.FirstOrDefault(x => x.Email == userEmail && x.Password == userPassword);
            }

            // Create Jwt.
            JwtSecurityToken CreateJwtToken()
            {
                var claims = CreateClaim(user);
                var credential = CreateJwtCredential();
                return new JwtSecurityToken(
                        issuer: "JwtIssuer",
                        audience: "JwtAudience",
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: credential
                    );
            }

            // Create claim body.
            List<Claim> CreateClaim(User user)
            {
                return new List<Claim>
                {
                    new Claim(nameof(user.Id), user.Id.ToString()),
                    new Claim(nameof(user.Name), user.Name),
                    new Claim(nameof(user.Email), user.Email),
                };
            }

            // Create Jwt credential.
            SigningCredentials CreateJwtCredential()
            {
                var key = CreateJwtKey();
                return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            }

            // Create jwt key from config secret key.
            SymmetricSecurityKey CreateJwtKey()
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
            }
        }
    }
}