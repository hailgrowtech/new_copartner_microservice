using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace BlogService.Commands;

public record CreateBlogCommand(Blog Blog) : IRequest<Blog>;

