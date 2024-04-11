using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record DeleteSubscriberCommand(Guid Id) : IRequest<Subscriber>;
}
