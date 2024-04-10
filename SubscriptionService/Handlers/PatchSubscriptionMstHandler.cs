using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Profiles;

namespace SubscriptionService.Handlers
{
    public class PatchSubscriptionMstHandler : IRequestHandler<PatchSubscriptionMstCommand, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchSubscriptionMstHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<SubscriptionMst> Handle(PatchSubscriptionMstCommand command, CancellationToken cancellationToken)
        {
            var subscriptionMstToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.SubscriptionMst);
            _dbContext.Update(subscriptionMstToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return subscriptionMstToUpdate;
        }
    }
}
