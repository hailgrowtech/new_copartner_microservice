using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class PutPaymentResponseHandler : IRequestHandler<PutPaymentResponseCommand, PaymentResponse>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutPaymentResponseHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentResponse> Handle(PutPaymentResponseCommand request, CancellationToken cancellationToken)
        {
            var entity = request.PaymentResponse;

            var existingEntity = await _dbContext.PaymentResponses.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }
    }
}
