using FluentValidation;
using WebAPI.Application.Features.Login;

namespace WebAPI.Application.Validations
{
    public class LoginValidation : AbstractValidator<LoginCommand>
    {
        public LoginValidation()
        {
            RuleFor(command => command.Email).NotEmpty().NotNull();
            RuleFor(command => command.Password).NotEmpty().NotNull();
        }
    }
}
