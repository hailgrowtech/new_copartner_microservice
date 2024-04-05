using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Data;
using UserService.Models;

namespace UserService.Handlers;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, User>
{
    private readonly UserDbContext _dbContext;
    public DeleteUserHandler(UserDbContext dbContext) => _dbContext = dbContext;
    public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userList = await _dbContext.Users.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (userList == null) return null;
        _dbContext.Users.Remove(userList);
        await _dbContext.SaveChangesAsync();
        return userList;
    }
}
