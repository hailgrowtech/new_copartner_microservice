using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class DeleteSubscriptionMstHandler : IRequestHandler<DeleteSubscriptionMstCommand, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteSubscriptionMstHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<SubscriptionMst> Handle(DeleteSubscriptionMstCommand request, CancellationToken cancellationToken)
        {
            var subscriptionMstsList = await _dbContext.subscriptionMsts.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (subscriptionMstsList == null) return null;
            _dbContext.subscriptionMsts.Remove(subscriptionMstsList);
            await _dbContext.SaveChangesAsync();
            return subscriptionMstsList;
        }
    }
}
