using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetSubscriptionByExpertsIdQuery(Guid Id) : IRequest<Subscription>;

}
