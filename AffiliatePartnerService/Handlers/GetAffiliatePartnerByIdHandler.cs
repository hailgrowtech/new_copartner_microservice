using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using MigrationDB.Data;
using AffiliatePartnerService.Queries;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers;

public class GetAffiliatePartnerByIdHandler : IRequestHandler<GetAffiliatePartnerByIdQuery, AffiliatePartner>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetAffiliatePartnerByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AffiliatePartner> Handle(GetAffiliatePartnerByIdQuery request, CancellationToken cancellationToken)
    {
        var affiliatePartnerList = await _dbContext.AffiliatePartners.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return affiliatePartnerList;
    }
}