using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Commands;
using UserService.Data;


namespace UserService.Handlers;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, User>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteUserHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userList = await _dbContext.Users.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (userList == null) return null;
        _dbContext.Users.Remove(userList);
        await _dbContext.SaveChangesAsync();
        return userList;
    }
}
