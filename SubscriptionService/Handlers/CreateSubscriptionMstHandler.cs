using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers
{
    public class CreateSubscriptionMstHandler : IRequestHandler<CreateSubscriptionMstCommand, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateSubscriptionMstHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionMst> Handle(CreateSubscriptionMstCommand request, CancellationToken cancellationToken)
        {
            var entity = request.SubscriptionMst;
            await _dbContext.subscriptionMsts.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.SubscriptionMst.Id = entity.Id;
            return request.SubscriptionMst;
        }
    }
}
