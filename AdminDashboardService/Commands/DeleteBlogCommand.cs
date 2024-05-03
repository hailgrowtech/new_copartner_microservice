using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record DeleteBlogCommand (Guid Id) : IRequest<Blog>;

}
