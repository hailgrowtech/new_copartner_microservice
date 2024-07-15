using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetMiniSubscriptionLinkByIdHandler : IRequestHandler<GetMiniSubscriptionLinkByIdQuery, MinisubscriptionLink>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetMiniSubscriptionLinkByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MinisubscriptionLink> Handle(GetMiniSubscriptionLinkByIdQuery request, CancellationToken cancellationToken)
        {
            var miniSubscriptionList = await _dbContext.MinisubscriptionLink.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return miniSubscriptionList;
        }
    }
}
