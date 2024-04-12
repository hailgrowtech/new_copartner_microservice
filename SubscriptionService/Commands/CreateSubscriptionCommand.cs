using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record CreateSubscriptionCommand(SubscriptionMst SubscriptionMst) : IRequest<SubscriptionMst>;
}
