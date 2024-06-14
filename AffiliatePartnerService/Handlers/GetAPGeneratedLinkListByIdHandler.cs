using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPGeneratedLinkListByIdHandler : IRequestHandler<GetAPGeneratedLinkListByIdQuery, IEnumerable<APGeneratedLinkReadDTO>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetAPGeneratedLinkListByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<APGeneratedLinkReadDTO>> Handle(GetAPGeneratedLinkListByIdQuery request, CancellationToken cancellationToken)
        {
            var apgeneratedlinklist = await _dbContext.APGeneratedLinks
                .Where(a => a.APId == request.Id && a.IsDeleted != true)
                .ToListAsync(cancellationToken: cancellationToken);


            var query = from ap in _dbContext.APGeneratedLinks

                        where ap.APId == request.Id && ap.IsDeleted != true
                        select new APGeneratedLinkReadDTO
                        {
                            Id = ap.Id,
                            APId = ap.APId,
                            GeneratedLink = ap.GeneratedLink,
                            APReferralLink = ap.APReferralLink,
                            Tag = ap.Tag
                        };

            return query;
        }
    }
}
