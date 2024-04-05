using MediatR;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Commands;

namespace AuthenticationService.Handlers;
public class GetUserByUserIdHandler : IRequestHandler<GetUserByUserIdQuery, Authentication>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserByUserIdHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<Authentication> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Authentications.Where(x => x.UserId == request.Guid).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}