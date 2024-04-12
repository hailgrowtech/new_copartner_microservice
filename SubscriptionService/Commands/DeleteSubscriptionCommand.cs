using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{

    public record DeleteSubscriptionCommand(Guid Id) : IRequest<SubscriptionMst>;
}
