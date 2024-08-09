using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands;

public record CreateSubscriberProcessCommand(Guid UserId, Guid SubscriberId) : IRequest<Subscriber>;
