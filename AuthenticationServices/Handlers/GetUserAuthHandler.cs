using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Handlers;

public class GetUserAuthHandler : IRequestHandler<GetUserAuthQuery, Authentication>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserAuthHandler(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Authentication?> Handle(GetUserAuthQuery request, CancellationToken cancellationToken)
    {
        if (request.Authentication != null)
        {
            return await _dbContext.Authentications.Where(x => x.UserId == request.Authentication.UserId).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }
        else
        {
            return await _dbContext.Authentications.Where(u => u.RefreshTokens.Any(t => t.Token == request.Token)).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}