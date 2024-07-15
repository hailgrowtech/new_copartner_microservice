using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetMiniSubscripionLinkQuery : IRequest<IEnumerable<MinisubscriptionLink>>;
}
