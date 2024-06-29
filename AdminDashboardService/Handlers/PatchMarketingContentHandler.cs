using MediatR;
using MigrationDB.Data;
using AdminDashboardService.Commands;
using MigrationDB.Model;
using AdminDashboardService.Profiles;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.CommonModels;

namespace AdminDashboardService.Handlers;
public class PatchMarketingContentHandler : IRequestHandler<PatchMarketingServiceCommand, MarketingContent>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;

    public PatchMarketingContentHandler(CoPartnerDbContext dbContext,
                            IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }

    public async Task<MarketingContent> Handle(PatchMarketingServiceCommand command, CancellationToken cancellationToken)
    {
        // Fetch the current entity from the database without tracking it
        var currentMarketingContent = await _dbContext.MarketingContents.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentMarketingContent == null)
        {
            // Handle the case where the subscriber does not exist
            throw new Exception($"Marketing Content with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var MarketingContentToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentMarketingContent);
        MarketingContentToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.MarketingContents.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(MarketingContentToUpdate);
        _dbContext.Entry(MarketingContentToUpdate).State = EntityState.Modified;
        // Preserve multiple properties 
        _dbContext.PreserveProperties(trackedEntity, currentMarketingContent, "CreatedOn");

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MarketingContentToUpdate;

    }
}