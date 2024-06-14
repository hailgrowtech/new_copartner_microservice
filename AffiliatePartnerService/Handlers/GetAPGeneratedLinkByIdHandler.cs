using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers
{
    public class GetAPGeneratedLinkByIdHandler : IRequestHandler<GetAPGeneratedLinkByIdQuery, APGeneratedLinks>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetAPGeneratedLinkByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<APGeneratedLinks> Handle(GetAPGeneratedLinkByIdQuery request, CancellationToken cancellationToken)
        {
            var apGeneratedLinkList = await _dbContext.APGeneratedLinks.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return apGeneratedLinkList;
        }
    }
}
