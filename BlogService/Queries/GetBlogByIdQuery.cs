using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace BlogService.Queries;
public record GetBlogByIdQuery(Guid Id) : IRequest<Blog>;


 
