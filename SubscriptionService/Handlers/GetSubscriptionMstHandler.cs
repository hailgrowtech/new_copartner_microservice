using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionMstHandler : IRequestHandler<GetSubscriptionMstQuery, IEnumerable<SubscriptionMst>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionMstHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<SubscriptionMst>> Handle(GetSubscriptionMstQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.subscriptionMsts.ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }
    }
}
