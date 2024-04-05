using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Queries;
public record GetUserAuthQuery(Authentication Authentication, string Token) : IRequest<Authentication>;