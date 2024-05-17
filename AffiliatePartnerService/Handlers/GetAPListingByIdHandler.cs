using AffiliatePartnerService.Dtos;
using ExpertService.Queries;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace ExpertsService.Handlers;
public class GetAPListingByIdHandler : IRequestHandler<GetAPListingByIdQuery, IEnumerable<APListingDataDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetAPListingByIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<APListingDataDto>> Handle(GetAPListingByIdQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.page - 1) * request.pageSize;
        var query = from wallet in _dbContext.Wallets
                    where wallet.AffiliatePartnerId == request.Id
                    join subscriber in _dbContext.Subscribers on wallet.SubscriberId equals subscriber.Id into subscriberJoin
                    from sub in subscriberJoin.DefaultIfEmpty()
                    join user in _dbContext.Users on sub.UserId equals user.Id into userJoin
                    from usr in userJoin.DefaultIfEmpty()
                    join affiliatePartner in _dbContext.AffiliatePartners on usr.AffiliatePartnerId equals affiliatePartner.Id into affiliatePartnerJoin
                    from aff in affiliatePartnerJoin.DefaultIfEmpty()
                    join expert in _dbContext.Experts on wallet.ExpertsId equals expert.Id into expertJoin
                    from exp in expertJoin.DefaultIfEmpty()
                    join subscription in _dbContext.Subscriptions on sub.SubscriptionId equals subscription.Id into subscriptionJoin
                    from subscr in subscriptionJoin.DefaultIfEmpty()
                    select new APListingDataDto
                    {
                        APName = aff.Name,
                        ReferralLink = aff.ReferralLink,
                        Date = sub.CreatedOn,
                        UserMobileNo = usr.MobileNumber,
                        RAName = aff.Id.ToString().Length > 10 ? aff.Name : (usr.AffiliatePartnerId.ToString().Length > 5 ? "SELF" : "ORGANIC"),
                        Amount = wallet.RAAmount,
                        Subscription = subscr.ServiceType
                    };

        var result = await query.Skip(skip).Take(request.pageSize).ToListAsync(cancellationToken);
        return result;
    }
}

