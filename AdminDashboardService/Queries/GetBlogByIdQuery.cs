using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetBlogByIdQuery(Guid Id) : IRequest<Blog>;


 
