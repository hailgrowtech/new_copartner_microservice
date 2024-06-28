using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriberHandler : IRequestHandler<GetSubscriberQuery, IEnumerable<Subscriber>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriberHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<Subscriber>> Handle(GetSubscriberQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Subscribers.Where(x=> x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            // Convert all DateTime fields to IST
            return entities.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
