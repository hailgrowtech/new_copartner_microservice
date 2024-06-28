using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers;

public class GetPaymentResponseByStatusHandler : IRequestHandler<GetPaymentResponseByStatusQuery, IEnumerable<PaymentResponse>>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetPaymentResponseByStatusHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PaymentResponse>> Handle(GetPaymentResponseByStatusQuery request, CancellationToken cancellationToken)
    {
        var payments = await _dbContext.PaymentResponses
            .Include(s => s.Subscriptions)
            .Include(s => s.Users)
            .Where(a => a.Status == request.paymentStatus && !a.IsDeleted)
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync(cancellationToken);

        return payments;
    }

}
