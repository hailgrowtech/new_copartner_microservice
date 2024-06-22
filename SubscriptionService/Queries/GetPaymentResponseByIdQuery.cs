using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries
{
    public record GetPaymentResponseByIdQuery(Guid Id) : IRequest<PaymentResponse>;

}
