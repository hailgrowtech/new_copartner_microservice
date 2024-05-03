using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Models;
using MigrationDB.Data;
using AdminDashboardService.Queries;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers;

public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, Blog>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetBlogByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Blog> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var expertsList = await _dbContext.Blogs.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return expertsList;
    }
}