using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateSubscriptionHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionMst> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = request.SubscriptionMst;
            await _dbContext.Subscriptions.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.SubscriptionMst.Id = entity.Id;
            return request.SubscriptionMst;
        }
    }
}
