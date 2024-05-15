using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

using AuthenticationService.Queries;

namespace AuthenticationService.Handlers;
public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserByEmailHandler(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthenticationDetail> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var obj = await _dbContext.AuthenticationDetails.Where(x => x.Email == request.Users.Email).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return obj;
    }
}