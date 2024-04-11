using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{

    public record GetSubscriberQuery : IRequest<IEnumerable<Subscriber>>;

}
