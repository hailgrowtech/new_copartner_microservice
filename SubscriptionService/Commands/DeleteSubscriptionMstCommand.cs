using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{

    public record DeleteSubscriptionMstCommand(Guid Id) : IRequest<SubscriptionMst>;
}
