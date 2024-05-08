using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using CommonLibrary;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPListingHandler : IRequestHandler<GetAPListingQuery, IEnumerable<APListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetAPListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<APListingDto>> Handle(GetAPListingQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users
    .Where(u => u.ReferralMode == "AP")
    .GroupJoin(_dbContext.AffiliatePartners.Where(ap => !ap.IsDeleted),
        user => user.AffiliatePartnerId,
        affiliatePartner => affiliatePartner.Id,
        (user, affiliatePartner) => new { user, affiliatePartner })
    .SelectMany(
        ua => ua.affiliatePartner.DefaultIfEmpty(),
        (ua, affiliatePartner) => new { ua.user, affiliatePartner })
    .GroupJoin(_dbContext.Subscribers,
        ua => ua.user.Id,
        subscriber => subscriber.UserId,
        (ua, subscribers) => new { ua.user, ua.affiliatePartner, subscribers })
    .SelectMany(
        uas => uas.subscribers.DefaultIfEmpty(),
        (uas, subscriber) => new { uas.user, uas.affiliatePartner, subscriber })
    .GroupJoin(_dbContext.Wallets,
        uas => uas.subscriber.Id,
        wallet => wallet.SubscriberId,
        (uas, wallets) => new { uas.user, uas.affiliatePartner, uas.subscriber, wallets })
    .SelectMany(
        uasw => uasw.wallets.DefaultIfEmpty(),
        (uasw, wallet) => new { uasw.user, uasw.affiliatePartner, uasw.subscriber, wallet })
    .GroupBy(
        result => new { APName = result.affiliatePartner.Name, result.user.ReferralMode })
    .Select(group => new APListingDto
    {
        APName = group.Key.APName,
        UsersCount = group.Select(g => g.user.Id).Distinct().Count(),
        UsersPayment = group.Select(g => g.subscriber.UserId).Distinct().Count(),
        RAEarning = group.Sum(g => g.wallet.RAAmount),
        APEarning = group.Sum(g => g.wallet.APAmount),
        CPEarning = group.Sum(g => g.wallet.CPAmount)
    });


            var result = await query.ToListAsync();
            return result;

            //return result.Select(item => new APListingDto
            //{
            //    APName = item.APName,
            //    UsersCount = item.UserCount,
            //    UsersPayment = item.UserPay,
            //    RAEarning = item.RAEarning,
            //    APEarning = item.APEarning,
            //    CPEarning = item.CPEarning
            //});
        }

    }
}
