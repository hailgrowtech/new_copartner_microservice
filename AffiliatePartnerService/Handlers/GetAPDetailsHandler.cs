using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPDetailsHandler : IRequestHandler<GetAPDetailsQuery, IEnumerable<APDetailDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetAPDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<APDetailDto>> Handle(GetAPDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = from ap in _dbContext.AffiliatePartners
                        where !ap.IsDeleted
                        join u in _dbContext.Users.Where(u => u.ReferralMode == "AP" && !u.IsDeleted)
                            on ap.Id equals u.AffiliatePartnerId into users
                        from user in users.DefaultIfEmpty()
                        join sub in _dbContext.Subscribers
                            on user.Id equals sub.UserId into subscribers
                        from subscriber in subscribers.DefaultIfEmpty()
                        join wallet in _dbContext.Wallets
                            on subscriber.Id equals wallet.SubscriberId into wallets
                        from wallet in wallets.DefaultIfEmpty()
                        group new { ap, wallet } by new
                        {
                            ap.Id,
                            ap.CreatedOn,
                            ap.Name,
                            ap.MobileNumber,
                            ap.FixCommission1,
                            ap.FixCommission2
                        } into g
                        select new APDetailDto
                        {
                            Id = g.Key.Id,
                            JoinDate = g.Key.CreatedOn,
                            APName = g.Key.Name,
                            MobileNumber = g.Key.MobileNumber,
                            FixCommission1 = g.Key.FixCommission1,
                            FixCommission2 = g.Key.FixCommission2,
                            APEarning = g.Sum(w => w.wallet.APAmount ?? 0) // Handle null values
                        };

            var result = await query.ToListAsync(cancellationToken);
            return result;
        }


    }
}
