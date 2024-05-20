using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionByExpertsIdHandler : IRequestHandler<GetSubscriptionByExpertsIdQuery, Subscription>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionByExpertsIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscription> Handle(GetSubscriptionByExpertsIdQuery request, CancellationToken cancellationToken)
        {
            var subscriptionMstsList = await _dbContext.Subscriptions.Where(a => a.ExpertsId == request.Id && a.IsDeleted!= true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return subscriptionMstsList;
        }
    }

}
