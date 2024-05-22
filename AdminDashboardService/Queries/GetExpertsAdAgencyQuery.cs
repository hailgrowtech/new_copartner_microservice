using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetExpertsAdAgencyQuery : IRequest<IEnumerable<ExpertsAdvertisingAgency>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetExpertsAdAgencyQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


 
