using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record CreateSubscriberCommand(Subscriber Subscriber) : IRequest<Subscriber>;
}
