using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriberByLinkHandler : IRequestHandler<GetSubscriberByLinkQuery, IEnumerable<Subscriber>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriberByLinkHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<Subscriber>> Handle(GetSubscriberByLinkQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Subscribers
                .Include(s => s.User) // Ensure User is included in the query
                .Where(s => s.User.LandingPageUrl == request.Link && s.IsDeleted != true)
                .ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }
    }
}
