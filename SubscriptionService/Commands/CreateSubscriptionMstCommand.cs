using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record CreateSubscriptionMstCommand(SubscriptionMst SubscriptionMst) : IRequest<SubscriptionMst>;
}
