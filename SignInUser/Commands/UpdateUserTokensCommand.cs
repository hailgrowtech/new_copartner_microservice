using MediatR;
using SignInService.Models;

namespace SignInService.Commands;

public record UpdateUserTokensCommand(RefreshToken refreshToken, PotentialCustomer Lead, int RefreshTokenTTL) : IRequest<PotentialCustomer>;
 