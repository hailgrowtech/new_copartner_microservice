using MediatR;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Commands
{

    //public record CreateMiniSubscriptionLinkCommand(MinisubscriptionLink MinisubscriptionLink) : IRequest<MinisubscriptionLink>;

    public record CreateMiniSubscriptionLinkCommand(Guid? ExpertsId, Guid? SubscriptionId) : IRequest<MinisubscriptionLink>;



}
