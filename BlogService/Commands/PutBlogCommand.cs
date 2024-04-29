
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace BlogService.Commands
{
    public record PutBlogCommand(Blog blog) : IRequest<Blog>;
}
