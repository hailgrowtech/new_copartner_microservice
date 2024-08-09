using MediatR;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Commands;
using UserService.Data;


namespace UserService.Handlers;
public class CreateTempUserHandler : IRequestHandler<CreateTempUserCommand, TempUser>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateTempUserHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<TempUser> Handle(CreateTempUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = request.TempUser;
            await _dbContext.TempUsers.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.TempUser.Id = entity.Id;
            return request.TempUser;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}