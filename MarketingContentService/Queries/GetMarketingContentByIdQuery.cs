using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace MarketingContentService.Queries;
public record GetMarketingContentByIdQuery(Guid Id) : IRequest<MarketingContent>;


 
