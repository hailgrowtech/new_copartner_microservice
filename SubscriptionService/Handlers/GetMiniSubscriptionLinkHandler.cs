using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetMiniSubscriptionLinkHandler : IRequestHandler<GetMiniSubscripionLinkQuery, IEnumerable<MinisubscriptionLink>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetMiniSubscriptionLinkHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<MinisubscriptionLink>> Handle(GetMiniSubscripionLinkQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.MinisubscriptionLink.Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            // Convert all DateTime fields to IST
            return entities.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
