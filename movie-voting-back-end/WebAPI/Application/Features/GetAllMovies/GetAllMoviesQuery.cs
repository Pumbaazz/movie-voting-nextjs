using MediatR;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Features.GetAllMovies
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<Movies>>
    {
    }
}
