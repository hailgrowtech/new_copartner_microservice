
using MediatR;
using MigrationDB.Models;

namespace ExpertService.Commands
{
    public record PutExpertsCommand(Experts experts) : IRequest<Experts>;
}
