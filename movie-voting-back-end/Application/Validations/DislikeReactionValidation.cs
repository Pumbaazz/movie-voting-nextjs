using FluentValidation;
using WebAPI.Application.Features.Login;
using WebAPI.Application.Features.Reactions;

namespace WebAPI.Application.Validations
{
    public class DislikeReactionValidation : AbstractValidator<DislikeReactionCommand>
    {
        public DislikeReactionValidation()
        {
            RuleFor(command => command.UserId).NotEmpty().NotNull();
            RuleFor(command => command.MovieId).NotEmpty().NotNull();
        }
    }
}
