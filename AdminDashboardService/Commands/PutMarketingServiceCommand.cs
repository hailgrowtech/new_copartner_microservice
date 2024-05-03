
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record PutMarketingServiceCommand(MarketingContent MarketingContent) : IRequest<MarketingContent>;
}
