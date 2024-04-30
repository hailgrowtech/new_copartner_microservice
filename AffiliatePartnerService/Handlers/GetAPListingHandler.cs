using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using CommonLibrary;
using MediatR;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPListingHandler : IRequestHandler<GetAPListingQuery, IEnumerable<APListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetAPListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<APListingDto>> Handle(GetAPListingQuery request, CancellationToken cancellationToken)
        {
            var affiliatePartnerList = await _dbContext.AffiliatePartners.Where(a =>  a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);


            var aplisting = new APListingDto
            {
                APName = _dbContext.AffiliatePartners.Select(a => a.Name).SingleOrDefault(),
                UsersCount = /* _dbContext.Users.Select(a => a.ReferralMode == "AP" && a.IsDeleted != true).Count()*/ 0,
                APEarning = 0,
                RAEarning = 0,
                CPEarning = 0      
            };

            //if (aplisting == null) return null;
            return new List<APListingDto> { aplisting };
        }
    }
}
