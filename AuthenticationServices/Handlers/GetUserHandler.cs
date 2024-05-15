using MediatR;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Data;
using AuthenticationService.Models;

using AuthenticationService.Queries;

namespace AuthenticationService.Handlers;
public class GetUserHandler : IRequestHandler<GetUserQuery, IEnumerable<AuthenticationDetail>>
{
    private readonly AuthenticationDbContext _dbContext;
    public GetUserHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<AuthenticationDetail>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.Page - 1) * request.PageSize;
        var entities =  await _dbContext.AuthenticationDetails.Where(x => x.IsActive == true && x.UserType==request.UserType).Skip(skip).Take(request.PageSize).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}