using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;

public record CreateMarketingServiceCommand(MarketingContent MarketingContent) : IRequest<MarketingContent>;

