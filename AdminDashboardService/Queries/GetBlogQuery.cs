using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetBlogQuery : IRequest<IEnumerable<Blog>>;


 
