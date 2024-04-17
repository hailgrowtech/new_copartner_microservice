using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Data;

using UserService.Queries;

namespace UserService.Handlers;
public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUsersHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.Users.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}