using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetSubscriptionByIdQuery(Guid Id) : IRequest<SubscriptionMst>;

}
