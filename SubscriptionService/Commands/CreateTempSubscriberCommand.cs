using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands;

public record CreateTempSubscriberCommand(TempSubscriber TempSubscriber) : IRequest<TempSubscriber>;
