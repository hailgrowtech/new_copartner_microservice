using ExpertService.Queries;
using ExpertsService.Dtos;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace ExpertsService.Handlers;
public class GetRAListingHandler : IRequestHandler<GetRAListingQuery, IEnumerable<RAListingDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetRAListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<RAListingDto>> Handle(GetRAListingQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.Page - 1) * request.PageSize;
        var query = _dbContext.Users
.GroupJoin(_dbContext.Subscribers,
    user => user.Id,
    subscriber => subscriber.UserId,
    (user, subscribers) => new { user, subscribers })
.SelectMany(
    us => us.subscribers.DefaultIfEmpty(),
    (us, subscriber) => new { us.user, subscriber })
.GroupJoin(_dbContext.Wallets,
    us => us.subscriber.Id,
    wallet => wallet.SubscriberId,
    (us, wallets) => new { us.user, us.subscriber, wallets })
.SelectMany(
    usw => usw.wallets.DefaultIfEmpty(),
    (usw, wallet) => new { usw.user, usw.subscriber, wallet })
.GroupJoin(_dbContext.Subscriptions,
    usw => usw.subscriber.SubscriptionId,
    subscription => subscription.Id,
    (usw, subscriptions) => new { usw.user, usw.subscriber, usw.wallet, subscriptions })
.SelectMany(
    usws => usws.subscriptions.DefaultIfEmpty(),
    (usws, subscription) => new { usws.user, usws.subscriber, usws.wallet, subscription })
.Join(_dbContext.Experts.Where(expert => !expert.IsDeleted),
    uswss => uswss.subscription.ExpertsId,
    expert => expert.Id,
    (uswss, expert) => new { uswss.user, uswss.subscriber, uswss.wallet, expert,uswss.subscription })
.Where(uew => uew.expert != null && uew.expert.IsDeleted == false)
.GroupBy(
    result => new { RAName = result.expert.Name,Id=result.subscription.ExpertsId, result.expert.IsDeleted })
.Select(group => new {
    Id=group.Key.Id,
    Name = group.Key. RAName,
    UserCount = group.Select(g => g.subscriber.Id).Distinct().Count(),
    RAEarning = group.Sum(g => g.wallet.RAAmount),
    CPEarning = group.Sum(g => g.wallet.CPAmount)
});

        var result = await query.Skip(skip).Take(request.PageSize).ToListAsync();

        return result.Select(expert => new RAListingDto
        {
            Id = expert.Id,
            Name = expert.Name,
            UsersCount = expert.UserCount,
            RAEarning = expert != null ? expert.RAEarning : Convert.ToDecimal(0),
            CPEarning = expert != null ? expert.CPEarning : Convert.ToDecimal(0)
        });

    }

}
