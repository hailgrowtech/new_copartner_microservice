using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetSubscriptionMstByIdQuery(Guid Id) : IRequest<SubscriptionMst>;

}
