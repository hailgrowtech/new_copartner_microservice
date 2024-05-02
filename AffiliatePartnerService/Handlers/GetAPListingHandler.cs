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
            var earnings = (from ap in _dbContext.AffiliatePartners
                            join u in _dbContext.Users.Where(u => u.ReferralMode == "AP") on ap.Id equals u.AffiliatePartnerId into userGroup
                            from ug in userGroup.DefaultIfEmpty()
                            join s in _dbContext.Subscribers on ug.Id equals s.UserId into subscriberGroup
                            from sg in subscriberGroup.DefaultIfEmpty()
                            join sub in _dbContext.Subscriptions on sg.SubscriptionId equals sub.Id into subscriptionGroup
                            from subg in subscriptionGroup.DefaultIfEmpty()
                            join e in _dbContext.Experts on subg.ExpertsId equals e.Id into expertGroup
                            from eg in expertGroup.DefaultIfEmpty()
                            group new { ap, sg, subg, eg } by new { ap.Name, sg.UserId, sg.TotalAmount, ap.FixCommission1, ap.FixCommission2, eg.FixCommission } into g
                            select new
                            {
                                APName = g.Key.Name,
                                APEarning = g.Sum(x => x.sg.TotalAmount > 0 ? x.sg.TotalAmount * (decimal)(x.ap.FixCommission1 / 100) : x.sg.TotalAmount * (decimal)(x.ap.FixCommission2 / 100)),
                                RAEarning = g.Sum(x => x.sg.TotalAmount > 0 ? x.sg.TotalAmount * (decimal)(x.eg.FixCommission / 100) : 0),
                                TotalAmount = g.Sum(x => x.sg.TotalAmount),
                                UsersCount = g.Select(x => x.sg.UserId).Distinct().Count(),
                                UsersPayment = g.Sum(x => x.sg.TotalAmount > 0 ? 1 : 0)
                            }).ToList();

            var result = from e in earnings
                         group e by e.APName into g
                         select new APListingDto
                         {
                             APName = g.Key,
                             APEarning = g.Sum(x => x.APEarning),
                             RAEarning = g.Sum(x => x.RAEarning),
                             CPEarning = g.Sum(x => x.TotalAmount) - (g.Sum(x => x.APEarning) + g.Sum(x => x.RAEarning)),
                             UsersCount = g.Sum(x => x.UsersCount),
                             UsersPayment = g.Sum(x => x.UsersPayment)
                         };

            return result.ToList();


            //var resultList = result.Select(x => new APListingDto
            //{
            //    APName = x.APName,
            //    UsersCount = x.TotalUsersCount,
            //    UsersPayment = x.TotalUsersPayment,
            //    APEarning = x.TotalAPEarning,
            //    RAEarning = x.TotalRAEarning,
            //    CPEarning = x.TotalCPEarning
            //}).ToList();

           // return result;
        }
    }
}
