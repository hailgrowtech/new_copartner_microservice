using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Data;

using UserService.Queries;

namespace UserService.Handlers;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUserByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userList = await _dbContext.Users.Where(a => a.Id == request.Id && a.IsDeleted!= true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return userList;
    }
}