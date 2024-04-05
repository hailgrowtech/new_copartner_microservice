using MediatR;
using UserService.Models;

namespace UserService.Queries;
public record GetUsersQuery : IRequest<IEnumerable<User>>;


 
