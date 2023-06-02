using FluentValidation;
using WebAPI.Application.Features.SignUp;

namespace WebAPI.Application.Validations
{
    public class SignUpValidation : AbstractValidator<SignUpCommand>
    {
        public SignUpValidation()
        {
            RuleFor(command => command.Email).NotEmpty().NotNull();
            RuleFor(command => command.Password).NotEmpty().NotNull();
            RuleFor(command => command.Name).NotEmpty().NotNull();
        }
    }
}
