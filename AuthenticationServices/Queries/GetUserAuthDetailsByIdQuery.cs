using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Queries;

public record GetUserAuthDetailsByIdQuery(Guid Guid) : IRequest<AuthenticationDetail>;



