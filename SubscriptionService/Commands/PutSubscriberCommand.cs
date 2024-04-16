using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record PutSubscriberCommand(Subscriber subscriber) : IRequest<Subscriber>;
}
