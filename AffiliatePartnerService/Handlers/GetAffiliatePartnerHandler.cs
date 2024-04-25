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
        var entities =  await _dbContext.AffiliatePartners.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}