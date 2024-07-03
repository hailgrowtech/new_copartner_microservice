using AdminDashboardService.Commands;
using AdminDashboardService.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using CommonLibrary.CommonModels;

namespace AdminDashboardService.Handlers
{
    public class PatchTelegramMessageHandler : IRequestHandler<PatchTelegramMessageCommand, TelegramMessage>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;

        public PatchTelegramMessageHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<TelegramMessage> Handle(PatchTelegramMessageCommand command, CancellationToken cancellationToken)
        {

            // Fetch the current entity from the database without tracking it
            var currentTelegramMessage = await _dbContext.TelegramMessages.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentTelegramMessage == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Telegram Message with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var TelegramMessageToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentTelegramMessage);
            TelegramMessageToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.TelegramMessages.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(TelegramMessageToUpdate);
            _dbContext.Entry(TelegramMessageToUpdate).State = EntityState.Modified;
            // Preserve multiple properties 
            _dbContext.PreserveProperties(trackedEntity, currentTelegramMessage, "CreatedOn");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return TelegramMessageToUpdate;

        }
    }
}
