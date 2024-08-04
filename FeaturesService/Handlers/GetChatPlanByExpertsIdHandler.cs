using CommonLibrary.Extensions;
using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetChatPlanByExpertsIdHandler : IRequestHandler<GetChatPlanByExpertsIdQuery, IEnumerable<ChatPlan>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetChatPlanByExpertsIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatPlan>> Handle(GetChatPlanByExpertsIdQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.page - 1) * request.pageSize;
            var chatPlan = await _dbContext.ChatPlans.Where(a => a.ExpertsId == request.Id && a.IsDeleted != true).Skip(skip).Take(request.pageSize).ToListAsync(cancellationToken);
            return chatPlan.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
