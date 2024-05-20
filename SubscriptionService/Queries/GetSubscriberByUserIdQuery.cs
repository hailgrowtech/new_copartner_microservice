using MediatR;
using MigrationDB.Model;
using System.Collections;

namespace SubscriptionService.Queries
{
    public record GetSubscriberByUserIdQuery(Guid Id) : IRequest<IEnumerable<Subscriber>>;
}
