using MediatR;
using Microsoft.EntityFrameworkCore;
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
        // Fetch the current entity from the database without tracking it
        var currentUsers = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentUsers == null)
        {
            // Handle the case where the expert does not exist
            throw new Exception($"User with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var usersToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentUsers);
        usersToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.Users.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(usersToUpdate);
        _dbContext.Entry(usersToUpdate).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return usersToUpdate;

    }
}