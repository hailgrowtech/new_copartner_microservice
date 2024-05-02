using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using MarketingContentService.Commands;

namespace MarketingContentService.Handlers;
public class DeleteMarketingContentHandler : IRequestHandler<DeleteMarketingServiceCommand, MarketingContent>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteMarketingContentHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<MarketingContent> Handle(DeleteMarketingServiceCommand request, CancellationToken cancellationToken)
    {
        var marketingContent = await _dbContext.MarketingContents.FindAsync(request.Id);
        if (marketingContent == null) return null; // or throw an exception indicating the entity not found

        marketingContent.IsDeleted = true; // Mark the entity as deleted
        marketingContent.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        marketingContent.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return marketingContent; // Return the deleted entity
    }
}
