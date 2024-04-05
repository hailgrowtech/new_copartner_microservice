using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Queries;
public record CheckIfNewTokenIsUniqueQuery(string NewToken) : IRequest<bool>;


 
