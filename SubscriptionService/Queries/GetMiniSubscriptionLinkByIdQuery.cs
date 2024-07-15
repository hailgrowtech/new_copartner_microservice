using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetMiniSubscriptionLinkByIdQuery(Guid Id) : IRequest<MinisubscriptionLink>;
}
