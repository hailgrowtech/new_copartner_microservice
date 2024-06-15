using SignInService.Data;
using SignInService.Models;
using SignInService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SignInService.Handlers;

public class GetUserAuthHandler : IRequestHandler<GetUserAuthQuery, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public GetUserAuthHandler(SignInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PotentialCustomer?> Handle(GetUserAuthQuery request, CancellationToken cancellationToken)
    {
        if (request.Lead != null)
        {
            return await _dbContext.Leads.Where(x => x.Id == request.Lead.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }
        else
        {
            return await _dbContext.Leads.Where(u => u.RefreshTokens.Any(t => t.Token == request.Token)).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}