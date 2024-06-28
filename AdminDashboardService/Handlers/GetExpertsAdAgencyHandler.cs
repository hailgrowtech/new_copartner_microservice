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
        int skip = (request.Page - 1) * request.PageSize;

        var entities =  await _dbContext.ExpertsAdvertisingAgencies.Where(x => x.IsDeleted != true)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip(skip)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken); if (entities == null) return null; 
        return entities;

    }
}