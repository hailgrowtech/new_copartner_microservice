using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetUserDataListingHandler : IRequestHandler<GetUserDataListingQuery, IEnumerable<UserDataListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetUserDataListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<UserDataListingDto>> Handle(GetUserDataListingQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;



            var result = await (from u in _dbContext.Users
                                where !u.IsDeleted
                                join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
                                from sub in gj.DefaultIfEmpty()
                                join e in _dbContext.Experts on u.ExpertsID equals e.Id into ej
                                from expert in ej.DefaultIfEmpty()
                                join ap in _dbContext.AffiliatePartners on u.AffiliatePartnerId equals ap.Id into apj
                                from affiliatePartner in apj.DefaultIfEmpty()
                                where sub == null
                                select new UserDataListingDto
                                 {
                                     UserId = u.Id,
                                     Date = u.CreatedOn, // Assuming CreatedOn is the registration date
                                     Name = u.Name,
                                     Mobile = u.MobileNumber,
                                     APId = u.AffiliatePartnerId,
                                     ExpertId = u.ExpertsID,
                                     ReferralMode = u.ReferralMode,
                                     ExpertName = expert.Name, // Assuming Experts table has a Name field
                                     AffiliatePartnerName = affiliatePartner.Name
                                })
                                .Skip(skip)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken);

            if (result == null) return null;
            return result;

        }
    }
}
