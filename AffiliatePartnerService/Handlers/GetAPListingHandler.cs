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
            var apName = await _dbContext.AffiliatePartners
                .Where(ap => !ap.IsDeleted)
                .Select(ap => ap.Name)
                .FirstOrDefaultAsync();

            var userCount = await _dbContext.Users
                .CountAsync(u => u.ReferralMode == "AP" && !u.IsDeleted);


            var userPayQuery = from sub in _dbContext.Subscribers
                               where !sub.IsDeleted
                               join wallet in _dbContext.Wallets on sub.Id equals wallet.SubscriberId into walletGroup
                               select new { SubscriberId = sub.Id, WalletCount = walletGroup.Select(w => w.Id).Distinct().Count() };


            var userPayResult = await userPayQuery.FirstOrDefaultAsync();
            var userPay = userPayResult != null ? userPayResult.WalletCount : 0;
            var subscriberId = userPayResult != null ? userPayResult.SubscriberId : Guid.Empty;

            var earnings = await _dbContext.Wallets
                .Where(w => !w.IsDeleted)
                .GroupBy(w => 1) // Grouping by a constant to get total sum
                .Select(g => new
                {
                    RAEarning = g.Sum(w => w.RAAmount),
                    APEarning = g.Sum(w => w.APAmount),
                    CPEarning = g.Sum(w => w.CPAmount)
                })
                .FirstOrDefaultAsync();

            var resultList = new List<APListingDto>
    {
        new APListingDto
        {
            APName = apName,
            UsersCount = userCount,
            UsersPayment = userPay,
            APEarning = earnings != null ? earnings.APEarning : 0,
            RAEarning = earnings != null ? earnings.RAEarning : 0,
            CPEarning = earnings != null ? earnings.CPEarning : 0
        }
    };

            return resultList;
        }
    }
}
