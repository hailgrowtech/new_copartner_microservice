using MediatR;
using FeaturesService.Commands;
using FeaturesService.Profiles;
using MigrationDB.Data;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.CommonModels;
using MigrationDB.Model;

namespace FeaturesService.Handlers;

public class PatchChatPlanHandler : IRequestHandler<PatchChatPlanCommand, ChatPlan>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchChatPlanHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<ChatPlan> Handle(PatchChatPlanCommand command, CancellationToken cancellationToken)
    {
        // Fetch the current entity from the database without tracking it
        var currentChatPlan = await _dbContext.ChatPlans.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentChatPlan == null)
        {
            // Handle the case where the expert does not exist
            throw new Exception($"Chat Plan with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var chatPlanToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentChatPlan);
        chatPlanToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.ChatPlans.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(chatPlanToUpdate);
        _dbContext.Entry(chatPlanToUpdate).State = EntityState.Modified;

        // Preserve multiple properties 
        _dbContext.PreserveProperties(chatPlanToUpdate, currentChatPlan, "CreatedOn");

        await _dbContext.SaveChangesAsync(cancellationToken);

        return chatPlanToUpdate;
    }
}
