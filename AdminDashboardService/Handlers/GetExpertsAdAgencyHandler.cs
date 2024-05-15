using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Queries;

namespace AdminDashboardService.Handlers;
public class GetExpertsAdAgencyHandler : IRequestHandler<GetExpertsAdAgencyQuery, IEnumerable<ExpertsAdvertisingAgency>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetExpertsAdAgencyHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<ExpertsAdvertisingAgency>> Handle(GetExpertsAdAgencyQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.ExpertsAdvertisingAgencies.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}