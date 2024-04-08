using MediatR;
using ExpertService.Models;
namespace ExpertService.Commands
{
    public record DeleteExpertsCommand (Guid Id) : IRequest<Experts>;

}
