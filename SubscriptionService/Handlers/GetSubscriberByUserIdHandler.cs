using MediatR;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;
using System.Collections;

namespace SubscriptionService.Handlers
{
    public class GetSubscriberByUserIdHandler : IRequestHandler<GetSubscriberByUserIdQuery, IEnumerable<Subscriber>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriberByUserIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Subscriber>> Handle(GetSubscriberByUserIdQuery request, CancellationToken cancellationToken)
        {
            var subscribersList = await _dbContext.Subscribers.Where(a => a.UserId == request.Id && a.IsDeleted!=true)
                .Include(s => s.User).Include(s=>s.Subscription).Include(s=>s.Subscription.Experts). ToListAsync(cancellationToken: cancellationToken);
            return subscribersList;
        }
    }
}
