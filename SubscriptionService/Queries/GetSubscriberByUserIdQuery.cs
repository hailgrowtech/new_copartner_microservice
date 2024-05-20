using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetSubscriberByUserIdQuery(Guid Id) : IRequest<Subscriber>;
}
