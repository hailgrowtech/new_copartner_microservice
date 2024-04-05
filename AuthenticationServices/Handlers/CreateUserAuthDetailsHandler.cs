using AuthenticationService.Commands;
using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;

namespace AuthenticationService.Handlers;
public class CreateUserAuthDetailsHandler : IRequestHandler<CreateUserAuthDetailsCommand, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public CreateUserAuthDetailsHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<AuthenticationDetail> Handle(CreateUserAuthDetailsCommand request, CancellationToken cancellationToken)
    {
        var entity = request.User;
        entity.Id = Guid.NewGuid();
        var user = _dbContext.AuthenticationDetails.Where(a => a.UserId == request.User.UserId).FirstOrDefault();
        if (user == null)
        {
            await _dbContext.AuthenticationDetails.AddAsync(entity);
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
