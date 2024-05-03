using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Queries;

namespace AdminDashboardService.Handlers;
public class GetAdAgencyDetailsHandler : IRequestHandler<GetAdAgencyDetailsQuery, IEnumerable<AdvertisingAgency>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetAdAgencyDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<AdvertisingAgency>> Handle(GetAdAgencyDetailsQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.AdvertisingAgencies.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}