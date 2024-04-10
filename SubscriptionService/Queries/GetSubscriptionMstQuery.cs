using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{

    public record GetSubscriptionMstQuery : IRequest<IEnumerable<SubscriptionMst>>;
}
