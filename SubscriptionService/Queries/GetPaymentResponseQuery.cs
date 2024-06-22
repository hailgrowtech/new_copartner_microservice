using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{

    public record GetPaymentResponseQuery : IRequest<IEnumerable<PaymentResponse>>;
}
