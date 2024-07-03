using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetTelegramMessageHandler : IRequestHandler<GetTelegramMessageQuery, IEnumerable<TelegramMessage>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetTelegramMessageHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<TelegramMessage>> Handle(GetTelegramMessageQuery request, CancellationToken cancellationToken)
        {
            // Calculate the number of records to skip
            int skip = (request.Page - 1) * request.PageSize;

            // Retrieve the page of wallets
            var entities = await _dbContext.TelegramMessages
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;
        }

    }
}
