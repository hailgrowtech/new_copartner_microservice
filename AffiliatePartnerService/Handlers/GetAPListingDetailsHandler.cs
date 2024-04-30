using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPListingDetailsHandler : IRequestHandler<GetAPListingDetailsQuery, IEnumerable<APListingDetailDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetAPListingDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<APListingDetailDto>> Handle(GetAPListingDetailsQuery request, CancellationToken cancellationToken)
        {
            var affiliatePartnerList = await _dbContext.AffiliatePartners.Where(a => a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);


            var aplisting = new APListingDetailDto
            {
                JoinDate = _dbContext.AffiliatePartners.Select(a => a.CreatedOn).SingleOrDefault(),
                Name = _dbContext.AffiliatePartners.Select(a => a.Name).SingleOrDefault(),
                MobileNumber = _dbContext.AffiliatePartners.Select(a => a.MobileNumber).SingleOrDefault(),
                FixCommission1 = _dbContext.AffiliatePartners.Select(a => a.FixCommission1).SingleOrDefault(),
                FixCommission2 = _dbContext.AffiliatePartners.Select(a => a.FixCommission2).SingleOrDefault(),
                Spend = 0
            };

            //if (aplisting == null) return null;
            return new List<APListingDetailDto> { aplisting };
        }
    }
}
