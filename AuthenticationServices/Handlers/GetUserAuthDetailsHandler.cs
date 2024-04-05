using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Handlers;
public class GetUserAuthDetailsHandler : IRequestHandler<GetUserAuthDetailsQuery, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserAuthDetailsHandler(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthenticationDetail?> Handle(GetUserAuthDetailsQuery request, CancellationToken cancellationToken)
    {
        if (request.AuthenticationDetail != null)
        {
            return await _dbContext.AuthenticationDetails.Where(a => a.MobileNumber == request.AuthenticationDetail.MobileNumber || a.Email == request.AuthenticationDetail.Email)
                                                          .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }
        return null;
    }
}