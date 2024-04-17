using MediatR;
using MigrationDB.Models;


namespace UserService.Queries;
public record GetUsersQuery : IRequest<IEnumerable<User>>;


 
