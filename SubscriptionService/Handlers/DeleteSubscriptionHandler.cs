using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteSubscriptionHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<SubscriptionMst> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscriptionMstsList = await _dbContext.Subscriptions.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (subscriptionMstsList == null) return null;
            _dbContext.Subscriptions.Remove(subscriptionMstsList);
            await _dbContext.SaveChangesAsync();
            return subscriptionMstsList;
        }
    }
}
