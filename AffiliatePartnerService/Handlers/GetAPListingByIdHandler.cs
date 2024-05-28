using AffiliatePartnerService.Dtos;
using ExpertService.Queries;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers;
public class GetAPListingByIdHandler : IRequestHandler<GetAPListingByIdQuery, IEnumerable<APListingDataDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetAPListingByIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<APListingDataDto>> Handle(GetAPListingByIdQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.page - 1) * request.pageSize;
        var query = from usr in _dbContext.Users
                    where usr.AffiliatePartnerId == request.Id
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
                    select new APListingDataDto
                    {
                        APName = aff.Name,
                        ReferralLink = aff.ReferralLink,
                        UserJoiningDate = usr.CreatedOn,
                        SubscribeDate = sub.CreatedOn,
                        UserMobileNo = usr.MobileNumber,
                        RAName = exp.Name,
                        Amount = wallet.RAAmount,
                        Subscription = subscr.ServiceType ?? "0", // Handle potential NULL value
                        LegalName = aff.LegalName,
                        GST = aff.GST
                    };

        var result = await query.Skip(skip).Take(request.pageSize).ToListAsync(cancellationToken);
        return result;
    }
}

