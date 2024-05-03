using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetMarketingContentByIdQuery(Guid Id) : IRequest<MarketingContent>;


 
