using MediatR;
using MigrationDB.Data;
using MarketingContentService.Commands;
using MigrationDB.Model;
using MarketingContentService.Profiles;

namespace MarketingContentService.Handlers;
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
        var marketingContentToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.MarketingContent);
        _dbContext.Update(marketingContentToUpdate);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return marketingContentToUpdate;
    }
}