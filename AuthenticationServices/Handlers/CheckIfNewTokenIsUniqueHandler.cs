using MediatR;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Handlers;
public class CheckIfNewTokenIsUniqueHandler : IRequestHandler<CheckIfNewTokenIsUniqueQuery, bool>
{
    private readonly AuthenticationDbContext _dbContext;
    public CheckIfNewTokenIsUniqueHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<bool> Handle(CheckIfNewTokenIsUniqueQuery request, CancellationToken cancellationToken)
    {
        // how to show last logged date time to user. 
        
        var isTokenUnique = !await _dbContext.Authentications.AnyAsync(u => u.RefreshTokens.Any(t => t.Token == request.NewToken),cancellationToken: cancellationToken);

        return isTokenUnique;
    }
}