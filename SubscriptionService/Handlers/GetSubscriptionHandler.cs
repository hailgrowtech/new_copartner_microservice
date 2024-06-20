using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionHandler : IRequestHandler<GetSubscriptionQuery, IEnumerable<Subscription>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Subscriptions.Include(s => s.Experts).Where(x=> x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            // Convert all DateTime fields to IST
            return entities.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
