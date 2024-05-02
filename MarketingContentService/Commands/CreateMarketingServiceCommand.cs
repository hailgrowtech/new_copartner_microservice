using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace MarketingContentService.Commands;

public record CreateMarketingServiceCommand(MarketingContent MarketingContent) : IRequest<MarketingContent>;

