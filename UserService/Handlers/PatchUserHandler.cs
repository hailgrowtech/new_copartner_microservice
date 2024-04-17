using MediatR;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Commands;
using UserService.Data;

using UserService.Profiles;

namespace UserService.Handlers;
public class PatchUserHandler : IRequestHandler<PatchUserCommand, User>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;
    public PatchUserHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<User> Handle(PatchUserCommand command, CancellationToken cancellationToken)
    {
        var userToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.User);
        _dbContext.Update(userToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return userToUpdate;
    }
}