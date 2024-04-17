using MediatR;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Commands;
using UserService.Data;


namespace UserService.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateUserHandler(CoPartnerDbContext dbContext)
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