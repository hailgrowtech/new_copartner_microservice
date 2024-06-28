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
            int skip = (request.Page - 1) * request.PageSize;
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
         result => new { result.affiliatePartner.Name, result.affiliatePartner.Id })
     .Select(group => new APListingDto
     {
         Id = group.Key.Id,
         APName = group.Key.Name,
         UsersCount = group.Select(g => g.user.Id).Distinct().Count(),
         UsersPayment = group.Where(g => g.subscriber != null).Select(g => g.subscriber.UserId).Count(),
         RAEarning = group.Sum(g => g.wallet.RAAmount ?? 0),
         APEarning = group.Sum(g => g.wallet.APAmount ?? 0),
         CPEarning = group.Sum(g => g.wallet.CPAmount ?? 0)
     });

            var result = await query.Skip(skip).Take(request.PageSize).ToListAsync();
            return result;
        }
    }
}
