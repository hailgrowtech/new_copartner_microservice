using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record PutSubscriptionCommand(Subscription Subscription) : IRequest<Subscription>;
}
