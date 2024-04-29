using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Queries;
using MigrationDB.Data;
using MigrationDB.Models;
using BlogService.Queries;
using MigrationDB.Model;

namespace BlogService.Handlers;
public class GetBlogHandler : IRequestHandler<GetBlogQuery, IEnumerable<Blog>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetBlogHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<Blog>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.Blogs.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}