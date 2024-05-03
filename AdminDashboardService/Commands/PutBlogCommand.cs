
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record PutBlogCommand(Blog blog) : IRequest<Blog>;
}
