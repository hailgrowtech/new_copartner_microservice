using MediatR;
using AuthenticationService.Models;

namespace AuthenticationService.Commands;

public record UpdateAuthenticationTokensCommand(RefreshToken refreshToken, Authentication Authentication, int RefreshTokenTTL) : IRequest<Authentication>;
 