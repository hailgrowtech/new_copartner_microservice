using MediatR;
using Microsoft.EntityFrameworkCore;
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
            //var subscriptionMstToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.Subscription);
            //_dbContext.Update(subscriptionMstToUpdate);
            //await _dbContext.SaveChangesAsync(cancellationToken);
           // return subscriptionMstToUpdate;

            // Fetch the current entity from the database without tracking it
            var currentSubscription = await _dbContext.Subscriptions.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentSubscription == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Subscription with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var SubscriptionToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentSubscription);
            SubscriptionToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.Subscriptions.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(SubscriptionToUpdate);
            _dbContext.Entry(SubscriptionToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return SubscriptionToUpdate;
        }
    }
}
