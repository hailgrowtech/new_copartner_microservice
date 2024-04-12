using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand, Subscription>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteSubscriptionHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<Subscription> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _dbContext.Subscriptions.FindAsync(request.Id);
            if (subscription == null) return null; // or throw an exception indicating the entity not found
            
            subscription.IsDeleted = true; // Mark the entity as deleted
            subscription.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            subscription.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return subscription; // Return the deleted entity
        }
    }
}
