using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetPaymentResponseHandler : IRequestHandler<GetPaymentResponseQuery, IEnumerable<PaymentResponse>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetPaymentResponseHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<PaymentResponse>> Handle(GetPaymentResponseQuery request, CancellationToken cancellationToken)
        {
           
                var entities = await _dbContext.PaymentResponses
                 .Include(pr => pr.Users)
    .Include(pr => pr.Subscriptions) // Include the related Subscription entity
           // Include the related User entity
    .Where(pr => !pr.IsDeleted)     // Filter out deleted records
    .ToListAsync(cancellationToken);

                if (entities == null) return null;
                return entities;
          
        }
    }
}
