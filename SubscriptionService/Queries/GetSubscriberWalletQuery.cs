using Copartner;
using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries;
public record GetSubscriberWalletQuery (Guid Id) : IRequest<WalletEvent>;
