using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Profiles;

namespace SubscriptionService.Handlers
{
    public class PatchSubscriberHandler : IRequestHandler<PatchSubscriberCommand, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchSubscriberHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<Subscriber> Handle(PatchSubscriberCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentSubscriber = await _dbContext.Subscribers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentSubscriber == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"Subscriber with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var SubscriberToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentSubscriber);
            SubscriberToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.Subscribers.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(SubscriberToUpdate);
            _dbContext.Entry(SubscriberToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return SubscriberToUpdate;
        }
    }
}
