using MediatR;
using UserService.Commands;
using UserService.Data;
using UserService.Models;
using UserService.Profiles;

namespace UserService.Handlers;
public class PatchUserHandler : IRequestHandler<PatchUserCommand, User>
{
    private readonly UserDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;
    public PatchUserHandler(UserDbContext dbContext,
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