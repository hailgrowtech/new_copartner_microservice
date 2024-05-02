using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace MarketingContentService.Queries;
public record GetMarketingContentQuery : IRequest<IEnumerable<MarketingContent>>;


