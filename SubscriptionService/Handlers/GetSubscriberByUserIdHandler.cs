using MediatR;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriberByUserIdHandler : IRequestHandler<GetSubscriberByUserIdQuery, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriberByUserIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subscriber> Handle(GetSubscriberByUserIdQuery request, CancellationToken cancellationToken)
        {
            var subscribersList = await _dbContext.Subscribers.Where(a => a.UserId == request.Id && a.IsDeleted!=true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return subscribersList;
        }
    }
}
