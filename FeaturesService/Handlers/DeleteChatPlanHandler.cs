using FeaturesService.Commands;
using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class DeleteChatPlanHandler : IRequestHandler<DeleteChatPlanCommand, ChatPlan>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteChatPlanHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<ChatPlan> Handle(DeleteChatPlanCommand request, CancellationToken cancellationToken)
        {
            var chatPlan = await _dbContext.ChatPlans.FindAsync(request.Id);
            if (chatPlan == null) return null; // or throw an exception indicating the entity not found

            chatPlan.IsDeleted = true; // Mark the entity as deleted
            chatPlan.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            chatPlan.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return chatPlan; // Return the deleted entity
        }
    }
}
