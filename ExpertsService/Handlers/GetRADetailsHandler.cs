using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace ExpertsService.Handlers
{
    public class GetRADetailsHandler : IRequestHandler<GetRADetailsQuery, IEnumerable<RADetailsDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetRADetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<RADetailsDto>> Handle(GetRADetailsQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;
            var query = from expert in _dbContext.Experts
                        where !expert.IsDeleted
                        join subscription in _dbContext.Subscriptions
                            on expert.Id equals subscription.ExpertsId into expertSubscriptions
                        from expertSubscription in expertSubscriptions.DefaultIfEmpty()
                        join subscriber in _dbContext.Subscribers
                            on expertSubscription.Id equals subscriber.SubscriptionId into subscriptionSubscribers
                        from subscriptionSubscriber in subscriptionSubscribers.DefaultIfEmpty()
                        join wallet in _dbContext.Wallets
                            on subscriptionSubscriber.Id equals wallet.SubscriberId into subscriberWallets
                        from subscriberWallet in subscriberWallets.DefaultIfEmpty()
                        group new { expert, subscriberWallet } by new
                        {
                            expert.Id,
                            expert.CreatedOn,
                            expert.Name,
                            expert.MobileNumber,
                            expert.FixCommission,
                            expert.IsDeleted
                        } into g
                        where !g.Key.IsDeleted
                        select new RADetailsDto
                        {
                            Id = g.Key.Id,
                            JoinDate = g.Key.CreatedOn,
                            Name = g.Key.Name,
                            SEBINo = g.Select(x => x.expert.SEBIRegNo).SingleOrDefault(),
                            FixCommission = g.Key.FixCommission,
                            RAEarning = g.Sum(x => x.subscriberWallet.RAAmount),
                            
                        };

            var result = await query.Skip(skip).Take(request.PageSize).ToListAsync();

            return result;
        }


    }
}
