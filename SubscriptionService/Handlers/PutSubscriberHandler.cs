using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class PutSubscriberHandler : IRequestHandler<PutSubscriberCommand, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutSubscriberHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscriber> Handle(PutSubscriberCommand request, CancellationToken cancellationToken)
        {
            var entity = request.subscriber;

            var existingEntity = await _dbContext.Subscribers.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }
    }
}
