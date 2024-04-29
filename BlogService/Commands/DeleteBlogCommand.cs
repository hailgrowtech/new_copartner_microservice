using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace BlogService.Commands
{
    public record DeleteBlogCommand (Guid Id) : IRequest<Blog>;

}
