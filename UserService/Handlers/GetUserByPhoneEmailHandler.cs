using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Queries;

namespace UserService.Handlers;
public class GetUserByMobileEmailHandler : IRequestHandler<GetUserByMobileNumberOrEmailQuery, User>
{
    private readonly UserDbContext _dbContext;
    public GetUserByMobileEmailHandler(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByMobileNumberOrEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Where(x => x.Email == request.User.Email || x.MobileNumber == request.User.MobileNumber).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return user;
    }
}