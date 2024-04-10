using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriptionMstByIdHandler : IRequestHandler<GetSubscriptionMstByIdQuery, SubscriptionMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriptionMstByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionMst> Handle(GetSubscriptionMstByIdQuery request, CancellationToken cancellationToken)
        {
            var subscriptionMstsList = await _dbContext.subscriptionMsts.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return subscriptionMstsList;
        }
    }

}
