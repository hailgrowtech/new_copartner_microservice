using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class DeleteRelationshipManagerHandler : IRequestHandler<DeleteRelationshipManagerCommand, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteRelationshipManagerHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<RelationshipManager> Handle(DeleteRelationshipManagerCommand request, CancellationToken cancellationToken)
        {
            var relationshipManager = await _dbContext.RelationshipManagers.FindAsync(request.Id);
            if (relationshipManager == null) return null; // or throw an exception indicating the entity not found

            relationshipManager.IsDeleted = true; // Mark the entity as deleted
            relationshipManager.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            relationshipManager.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return relationshipManager; // Return the deleted entity
        }
    }
}


