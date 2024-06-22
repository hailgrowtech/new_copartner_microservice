using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class CreatePaymentResponseHandler : IRequestHandler<CreatePaymentResponseCommand, PaymentResponse>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreatePaymentResponseHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentResponse> Handle(CreatePaymentResponseCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Payment;
            await _dbContext.PaymentResponses.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.Payment.Id = entity.Id;
            return request.Payment;
        }
    }
}
