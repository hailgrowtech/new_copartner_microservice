using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record CreateSubscriptionCommand(Subscription Subscription) : IRequest<Subscription>;
}
