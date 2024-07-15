using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{

    public record DeleteMiniSubscriptionLinkCommand(Guid Id) : IRequest<MinisubscriptionLink>;

}
