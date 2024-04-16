using MediatR;
using MigrationDB.Models;

namespace ExpertService.Commands
{
    public record DeleteExpertsCommand (Guid Id) : IRequest<Experts>;

}
