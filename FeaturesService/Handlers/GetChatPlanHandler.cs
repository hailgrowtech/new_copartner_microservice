using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers;
public class GetChatPlanHandler : IRequestHandler<GetChatPlanQuery, IEnumerable<ChatPlan>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetChatPlanHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<ChatPlan>> Handle(GetChatPlanQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;
        var entities = await _dbContext.ChatPlans.Where(x => x.IsDeleted != true)
                     .OrderByDescending(x => x.CreatedOn)
                     .Skip(skip)
                     .Take(request.PageSize)
                     .ToListAsync(cancellationToken);
        if (entities == null) return null;
        return entities;
    }
}
