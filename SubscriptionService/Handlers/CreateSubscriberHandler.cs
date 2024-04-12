using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class CreateSubscriberHandler : IRequestHandler<CreateSubscriberCommand, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateSubscriberHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscriber> Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Subscriber;
            await _dbContext.Subscribers.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.Subscriber.Id = entity.Id;
            return request.Subscriber;
        }
    }
}
