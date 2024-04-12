using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class DeleteSubscriberHandler : IRequestHandler<DeleteSubscriberCommand, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteSubscriberHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<Subscriber> Handle(DeleteSubscriberCommand request, CancellationToken cancellationToken)
        {
            var subscribersList = await _dbContext.Subscribers.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (subscribersList == null) return null;
            _dbContext.Subscribers.Remove(subscribersList);
            await _dbContext.SaveChangesAsync();
            return subscribersList;
        }
    }
}
