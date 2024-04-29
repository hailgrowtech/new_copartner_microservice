using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace BlogService.Queries;
public record GetBlogQuery : IRequest<IEnumerable<Blog>>;


 
