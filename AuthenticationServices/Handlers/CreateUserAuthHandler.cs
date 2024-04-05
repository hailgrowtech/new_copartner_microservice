using AuthenticationService.Commands;
using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Handlers;
public class CreateUserAuthHandler : IRequestHandler<CreateUserAuthCommand, Authentication>
{
    private readonly AuthenticationDbContext _dbContext;
    public CreateUserAuthHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<Authentication> Handle(CreateUserAuthCommand request, CancellationToken cancellationToken)
    {
        var entity = request.User;
        entity.Id = Guid.NewGuid();
        var user = _dbContext.Authentications.Where(a => a.UserId == request.User.UserId).FirstOrDefault();
        if (user == null)
        {
            await _dbContext.Authentications.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.User.Id = entity.Id;
            return request.User;
        }
        else
        {
            //we need to send an error message back to UI and inform User that he is already registered with email ID. 
            return null;
        }
    }
}
