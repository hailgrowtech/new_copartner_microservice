using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class CreateTelegramMessageHandler : IRequestHandler<CreateTelegramMessageCommand, TelegramMessage>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateTelegramMessageHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TelegramMessage> Handle(CreateTelegramMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = request.telegramMessage;
            // Check if the title is unique
            //bool isUnique = await IsBlogTitleUnique(entity.Title);
            //if (!isUnique)
            //{
            //    return null;
            //}


            await _dbContext.TelegramMessages.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.telegramMessage.Id = entity.Id;
            return request.telegramMessage;
        }
    }
}
