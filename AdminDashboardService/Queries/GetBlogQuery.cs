using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetBlogQuery : IRequest<IEnumerable<Blog>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetBlogQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


 
