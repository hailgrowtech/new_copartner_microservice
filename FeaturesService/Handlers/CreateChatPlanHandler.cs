using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers;

public class CreateChatPlanHandler : IRequestHandler<CreateChatPlanCommand, ChatPlan>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateChatPlanHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChatPlan> Handle(CreateChatPlanCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ChatPlan;
        await _dbContext.ChatPlans.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.ChatPlan.Id = entity.Id;
        //request.Experts.isActive = entity.isActive;
        return request.ChatPlan;
    }
}
