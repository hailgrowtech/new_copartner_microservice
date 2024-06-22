using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries;
public record GetPaymentResponseByStatusQuery(string paymentStatus) : IRequest<IEnumerable<PaymentResponse>>;
