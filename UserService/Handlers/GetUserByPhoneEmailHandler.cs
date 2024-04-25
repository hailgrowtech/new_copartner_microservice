using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Data;

using UserService.Queries;

namespace UserService.Handlers;
public class GetUserByMobileEmailHandler : IRequestHandler<GetUserByMobileNumberOrEmailQuery, User>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUserByMobileEmailHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByMobileNumberOrEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Where(x => x.MobileNumber == request.User.MobileNumber).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return user;
    }
}