using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, Subscription>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateSubscriptionHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscription> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Subscription;
            await _dbContext.Subscriptions.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.Subscription.Id = entity.Id;
            return request.Subscription;
        }
    }
}
