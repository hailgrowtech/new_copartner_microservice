using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetAdAgencyDetailsQuery : IRequest<IEnumerable<AdAgencyDetailsDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetAdAgencyDetailsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


 
