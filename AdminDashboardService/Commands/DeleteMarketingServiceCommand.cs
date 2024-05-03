using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record DeleteMarketingServiceCommand (Guid Id) : IRequest<MarketingContent>;

}
