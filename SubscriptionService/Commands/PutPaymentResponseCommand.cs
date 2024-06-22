using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands;
public record PutPaymentResponseCommand(PaymentResponse PaymentResponse) : IRequest<PaymentResponse>;
