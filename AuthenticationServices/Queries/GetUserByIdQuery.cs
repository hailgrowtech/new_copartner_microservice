using AuthenticationService.Models;
using MediatR;


namespace AuthenticationService.Queries;
public record GetUserByIdQuery(Guid Id) : IRequest<AuthenticationDetail>;


 
