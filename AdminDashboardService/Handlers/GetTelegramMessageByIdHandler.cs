using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetTelegramMessageByIdHandler : IRequestHandler<GetTelegramMessageByIdQuery, TelegramMessage>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetTelegramMessageByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TelegramMessage> Handle(GetTelegramMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var telegramMessage = await _dbContext.TelegramMessages.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return telegramMessage;
        }
    }
}
