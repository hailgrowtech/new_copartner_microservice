using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Profiles;

namespace SubscriptionService.Handlers
{
    public class PatchSubscriptionHandler : IRequestHandler<PatchSubscriptionCommand, Subscription>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchSubscriptionHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<Subscription> Handle(PatchSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscriptionMstToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.Subscription);
            _dbContext.Update(subscriptionMstToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return subscriptionMstToUpdate;
        }
    }
}
