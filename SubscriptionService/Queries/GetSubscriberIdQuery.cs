using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetSubscriberIdQuery(Guid Id) : IRequest<Subscriber>;
}
