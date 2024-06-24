using MediatR;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetPaymentResponseByIdHandler : IRequestHandler<GetPaymentResponseByIdQuery, PaymentResponse>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetPaymentResponseByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentResponse> Handle(GetPaymentResponseByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentListList = await _dbContext.PaymentResponses
                .Include(s=>s.Subscriptions) .Include(s=>s.Users)
                .Where(a => a.Id == request.Id && a.IsDeleted!=true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return paymentListList;
        }
    }
}
