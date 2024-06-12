using ExpertService.Models;
using ExpertService.Queries;
using ExpertsService.Dtos;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using Ocelot.RequestId;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExpertsService.Handlers;
public class GetRAListingByIdHandler : IRequestHandler<GetRAListingByIdQuery, IEnumerable<RAListingDataDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetRAListingByIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<RAListingDataDto>> Handle(GetRAListingByIdQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.page - 1) * request.pageSize;
        var query1 = from usr in _dbContext.Users
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
                    where exp.Id == request.Id && (sub.isActive == true || sub.IsDeleted == false)
                     select new RAListingDataDto
                    {
                        RAName = exp.Name,
                        UserJoiningDate = usr.CreatedOn,
                        SubscribeDate = sub.CreatedOn,
                        UserMobileNo = usr.Id != null ? usr.MobileNumber : null,
                        User = usr,
                        APName = (aff.Id != null && aff.Id.ToString().Length > 10)
                             ? aff.Name
                             : ((usr.Id != null && usr.ExpertsID != null && usr.ExpertsID == subscr.ExpertsId)
                                 ? "SELF"
                                 : ((usr.Id != null && usr.ExpertsID != null && usr.ExpertsID.ToString().Length > 5)
                                     ? (_dbContext.Experts.FirstOrDefault(e => e.Id == usr.ExpertsID).Name)
                                     : "ORGANIC")),
                         Amount = wallet.RAAmount,
                        Subscription = subscr.ServiceType ?? "No Subscrption",
                        PlanType = subscr.PlanType ?? "No Plan",
                        TransactionId = sub.TransactionId,
                        SubscriptionAmount = subscr.Amount,
                        LegalName = exp.LegalName,
                        GST = exp.GST,
                        InvoiceId = sub.InvoiceId,
                        PremiumTelegramChannel = sub.PremiumTelegramChannel
                    };

        var query2 = from usr in _dbContext.Users
                     join sub in _dbContext.Subscribers on usr.Id equals sub.UserId into subscriberJoin
                     from sub in subscriberJoin.DefaultIfEmpty()
                     join wallet in _dbContext.Wallets on sub.Id equals wallet.SubscriberId into walletJoin
                     from wallet in walletJoin.DefaultIfEmpty()
                     join aff in _dbContext.AffiliatePartners on wallet.AffiliatePartnerId equals aff.Id into affiliatePartnerJoin
                     from aff in affiliatePartnerJoin.DefaultIfEmpty()
                     join exp in _dbContext.Experts on wallet.ExpertsId equals exp.Id into expertJoin
                     from exp in expertJoin.DefaultIfEmpty()
                     join subscr in _dbContext.Subscriptions on sub.SubscriptionId equals subscr.Id into subscriptionJoin
                     from subscr in subscriptionJoin.DefaultIfEmpty()
                     where usr.ExpertsID == request.Id
                        && usr.ExpertsID == subscr.ExpertsId
                        && exp.Name != null
                        && !usr.IsDeleted
                        && (sub.isActive || !sub.IsDeleted)
                     select new RAListingDataDto
                     {
                         RAName = exp.Name,
                         UserJoiningDate = usr.CreatedOn,
                         SubscribeDate = sub.CreatedOn,
                         UserMobileNo = usr.Id != null ? usr.MobileNumber : null,
                         User = usr,
                         APName = (aff.Id != null && aff.Id.ToString().Length > 10) ? aff.Name : ((usr.Id != null && usr.ExpertsID != null && usr.ExpertsID.ToString().Length > 5) ? "SELF" : "ORGANIC"),
                         Amount = wallet.RAAmount,
                         Subscription = subscr.ServiceType ?? "No Subscrption",
                         PlanType = subscr.PlanType ?? "No Plan",
                         TransactionId = sub.TransactionId,
                         SubscriptionAmount = subscr.Amount,
                         LegalName = exp.LegalName,
                         GST = exp.GST,
                         InvoiceId = sub.InvoiceId,
                         PremiumTelegramChannel = sub.PremiumTelegramChannel
                     };

        var result = await query1.Union(query2).Skip(skip).Take(request.pageSize).ToListAsync(cancellationToken);
        return result;
    }
}

