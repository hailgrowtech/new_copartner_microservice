using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers;

public class DeletePaymentResponseHandler : IRequestHandler<DeletePaymentResponseCommand, PaymentResponse>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeletePaymentResponseHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
    public async Task<PaymentResponse> Handle(DeletePaymentResponseCommand request, CancellationToken cancellationToken)
    {
        var paymentList = await _dbContext.PaymentResponses.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (paymentList == null) return null;
        _dbContext.PaymentResponses.Remove(paymentList);
        await _dbContext.SaveChangesAsync();
        return paymentList;
    }
}
