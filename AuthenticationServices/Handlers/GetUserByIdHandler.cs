using MediatR;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Data;
using AuthenticationService.Models;

using AuthenticationService.Queries;

namespace AuthenticationService.Handlers;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserByIdHandler(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthenticationDetail> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var stackholderList = await _dbContext.AuthenticationDetails.Where(a => a.Id == request.Id && a.IsActive== true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return stackholderList;
    }
}