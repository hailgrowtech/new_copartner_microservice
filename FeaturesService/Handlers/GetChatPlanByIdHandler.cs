using CommonLibrary.Extensions;
using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetChatPlanByIdHandler : IRequestHandler<GetChatPlanByIdQuery, ChatPlan>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetChatPlanByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatPlan> Handle(GetChatPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var chatPlan = await _dbContext.ChatPlans.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return chatPlan.ConvertAllDateTimesToIST();
        }
    }
}
