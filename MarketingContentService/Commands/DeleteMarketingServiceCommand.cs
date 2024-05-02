using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace MarketingContentService.Commands
{
    public record DeleteMarketingServiceCommand (Guid Id) : IRequest<MarketingContent>;

}
