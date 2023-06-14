using FluentValidation;
using WebAPI.Application.Features.Reactions;

namespace WebAPI.Application.Validations
{
    public class LikeReactionValidation : AbstractValidator<LikeReactionCommand>
    {
        public LikeReactionValidation()
        {
            RuleFor(command => command.MovieId).NotEmpty().NotNull();
            RuleFor(command => command.UserId).NotEmpty().NotNull();
        }
    }
}
