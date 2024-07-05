using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class DeleteTelegramMessageHandler : IRequestHandler<DeleteTelegramMessageCommand, TelegramMessage>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteTelegramMessageHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<TelegramMessage> Handle(DeleteTelegramMessageCommand request, CancellationToken cancellationToken)
        {
            var telegramMessage = await _dbContext.TelegramMessages.FindAsync(request.Id);
            if (telegramMessage == null) return null; // or throw an exception indicating the entity not found

            telegramMessage.IsDeleted = true; // Mark the entity as deleted
            telegramMessage.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            telegramMessage.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return telegramMessage; // Return the deleted entity
        }
    }
}
