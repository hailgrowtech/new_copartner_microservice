using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands;

public record CreateBlogCommand(Blog Blog) : IRequest<Blog>;

