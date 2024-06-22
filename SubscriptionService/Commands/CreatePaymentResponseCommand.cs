using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Commands;
public record CreatePaymentResponseCommand(PaymentResponse Payment) : IRequest<PaymentResponse>;
