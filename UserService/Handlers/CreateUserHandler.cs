using MediatR;
using UserService.Commands;
using UserService.Data;
using UserService.Models;

namespace UserService.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly UserDbContext _dbContext;
    public CreateUserHandler(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = request.User;
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.User.Id = entity.Id;
        return request.User;
    }
}