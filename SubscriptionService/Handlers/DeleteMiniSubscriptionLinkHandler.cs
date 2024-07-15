using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Dtos;

namespace SubscriptionService.Handlers
{
    public class DeleteMiniSubscriptionLinkHandler : IRequestHandler<DeleteMiniSubscriptionLinkCommand, MinisubscriptionLink>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteMiniSubscriptionLinkHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<MinisubscriptionLink> Handle(DeleteMiniSubscriptionLinkCommand request, CancellationToken cancellationToken)
        {
            var subscribersList = await _dbContext.MinisubscriptionLink.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (subscribersList == null) return null;
            _dbContext.MinisubscriptionLink.Remove(subscribersList);
            await _dbContext.SaveChangesAsync();
            return subscribersList;
            {
            }

        }
    }
}
