using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class DeleteJoinHandler : IRequestHandler<DeleteJoinCommand, Join>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteJoinHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<Join> Handle(DeleteJoinCommand request, CancellationToken cancellationToken)
        {
            var joins = await _dbContext.Joins.FindAsync(request.Id);
            if (joins == null) return null; // or throw an exception indicating the entity not found

            joins.IsDeleted = true; // Mark the entity as deleted
            joins.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            joins.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return joins; // Return the deleted entity
        }
    }
}
