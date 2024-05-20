using ExpertService.Queries;
using ExpertsService.Dtos;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace ExpertsService.Handlers;
public class GetRAListingByIdHandler : IRequestHandler<GetRAListingByIdQuery, IEnumerable<RAListingDataDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetRAListingByIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<RAListingDataDto>> Handle(GetRAListingByIdQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.page - 1) * request.pageSize;
        var query = from usr in _dbContext.Users
                    join sub in _dbContext.Subscribers on usr.Id equals sub.UserId into subscriberJoin
                    from sub in subscriberJoin.DefaultIfEmpty()
                    join wallet in _dbContext.Wallets on sub.Id equals wallet.SubscriberId into walletJoin
                    from wallet in walletJoin.DefaultIfEmpty()
                    join aff in _dbContext.AffiliatePartners on usr.AffiliatePartnerId equals aff.Id into affiliatePartnerJoin
                    from aff in affiliatePartnerJoin.DefaultIfEmpty()
                    join exp in _dbContext.Experts on wallet.ExpertsId equals exp.Id into expertJoin
                    from exp in expertJoin.DefaultIfEmpty()
                    join subscr in _dbContext.Subscriptions on sub.SubscriptionId equals subscr.Id into subscriptionJoin
                    from subscr in subscriptionJoin.DefaultIfEmpty()
                    select new RAListingDataDto
                    {
                        RAName = exp.Name,
                        Date = sub.CreatedOn,
                        UserMobileNo = usr.Id != null ? usr.MobileNumber : null,
                        APName = (aff.Id != null && aff.Id.ToString().Length > 10) ? aff.Name : ((usr.Id != null && usr.ExpertsID != null && usr.ExpertsID.ToString().Length > 5) ? "SELF" : "ORGANIC"),
                        Amount = wallet.RAAmount,
                        Subscription = subscr.ServiceType ?? "0"
                    };

        var result = await query.Skip(skip).Take(request.pageSize).ToListAsync(cancellationToken);
        return result;
    }
}

