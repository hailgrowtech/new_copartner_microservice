using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Queries;

namespace UserService.Handlers;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly UserDbContext _dbContext;
    public GetUserByIdHandler(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userList = await _dbContext.Users.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return userList;
    }
}