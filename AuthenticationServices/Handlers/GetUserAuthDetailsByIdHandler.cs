using MediatR;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Commands;

namespace AuthenticationService.Handlers;
public class GetUserAuthDetailsByIdHandler : IRequestHandler<GetUserAuthDetailsByIdQuery, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserAuthDetailsByIdHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<AuthenticationDetail> Handle(GetUserAuthDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.AuthenticationDetails.Where(x => x.UserId == request.Guid).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}