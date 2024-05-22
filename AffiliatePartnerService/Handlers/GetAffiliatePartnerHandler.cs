using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using AffiliatePartnerService.Queries;
using MigrationDB.Model;

namespace AffiliatePartnerService.Handlers;
public class GetAffiliatePartnerHandler : IRequestHandler<GetAffiliatePartnerQuery, IEnumerable<AffiliatePartner>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetAffiliatePartnerHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<AffiliatePartner>> Handle(GetAffiliatePartnerQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;


        var entities =  await _dbContext.AffiliatePartners.Where(x => x.IsDeleted != true).Skip(skip)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken);

        if (entities == null) return null; 
        return entities;
    }
}