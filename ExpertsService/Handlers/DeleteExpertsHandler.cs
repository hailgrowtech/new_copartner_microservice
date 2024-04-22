using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Commands;

using MigrationDB.Data;
using MigrationDB.Models;

namespace ExpertService.Handlers;
public class DeleteExpertsHandler : IRequestHandler<DeleteExpertsCommand, Experts>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteExpertsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<Experts> Handle(DeleteExpertsCommand request, CancellationToken cancellationToken)
    {
        var experts = await _dbContext.Experts.FindAsync(request.Id);
        if (experts == null) return null; // or throw an exception indicating the entity not found

        experts.IsDeleted = true; // Mark the entity as deleted
        experts.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        experts.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return experts; // Return the deleted entity
    }
}
