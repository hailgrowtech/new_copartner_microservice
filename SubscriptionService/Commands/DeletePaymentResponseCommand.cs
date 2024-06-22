using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands
{
    public record DeletePaymentResponseCommand(Guid Id) : IRequest<PaymentResponse>;
}
