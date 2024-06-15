using MediatR;
using Microsoft.EntityFrameworkCore;
using SignInService.Data;
using SignInService.Queries;

namespace SignInService.Handlers;
public class CheckIfNewTokenIsUniqueHandler : IRequestHandler<CheckIfNewTokenIsUniqueQuery, bool>
{
    private readonly SignInDbContext _dbContext;
    public CheckIfNewTokenIsUniqueHandler(SignInDbContext dbContext) => _dbContext = dbContext;
    public async Task<bool> Handle(CheckIfNewTokenIsUniqueQuery request, CancellationToken cancellationToken)
    {
        // how to show last logged date time to user. 
        
        var isTokenUnique = !await _dbContext.Leads.AnyAsync(u => u.RefreshTokens.Any(t => t.Token == request.NewToken),cancellationToken: cancellationToken);

        return isTokenUnique;
    }
}