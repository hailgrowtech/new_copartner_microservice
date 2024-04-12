using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{

    public record GetSubscriptionQuery : IRequest<IEnumerable<SubscriptionMst>>;
}
