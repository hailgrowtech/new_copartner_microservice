using MediatR;
using UserService.Models;

namespace UserService.Queries;
public record GetUserByIdQuery(Guid Id) : IRequest<User>;


 
