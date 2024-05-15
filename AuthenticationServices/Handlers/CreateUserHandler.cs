using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;
using AuthenticationService.Commands;


namespace AuthenticationService.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public CreateUserHandler(AuthenticationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthenticationDetail> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Users;
        await _dbContext.AuthenticationDetails.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Users.Id = entity.Id;
        return request.Users;
    }
}