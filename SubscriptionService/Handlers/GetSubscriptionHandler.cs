using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionHandler : IRequestHandler<GetSubscriptionQuery, IEnumerable<SubscriptionMst>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<SubscriptionMst>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Subscriptions.ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }
    }
}
