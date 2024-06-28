using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using AdminDashboardService.Queries;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers;
public class GetBlogHandler : IRequestHandler<GetBlogQuery, IEnumerable<Blog>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetBlogHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<Blog>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.Page - 1) * request.PageSize;

        // Retrieve the page of wallets
        var entities = await _dbContext.Blogs
            .Where(x => x.IsDeleted != true)
            //.OrderByDescending(x => x.CreatedOn)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        if (entities == null) return null;
        return entities;
    }
}