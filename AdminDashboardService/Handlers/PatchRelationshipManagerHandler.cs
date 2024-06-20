using AdminDashboardService.Commands;
using AdminDashboardService.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdminDashboardService.Handlers
{
    public class PatchRelationshipManagerHandler : IRequestHandler<PatchRelationshipManagerCommand, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;

        public PatchRelationshipManagerHandler(CoPartnerDbContext dbContext, IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<RelationshipManager> Handle(PatchRelationshipManagerCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentRelationshipManager = await _dbContext.RelationshipManagers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
             
            if (currentRelationshipManager == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Relationship Manager with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var RelationshipManagerToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentRelationshipManager);
            RelationshipManagerToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.RelationshipManagers.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(RelationshipManagerToUpdate);
            _dbContext.Entry(RelationshipManagerToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return RelationshipManagerToUpdate;

        }
    }
}
