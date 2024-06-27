using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Dtos;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers
{
    public class GetSubscriberByLinkHandler : IRequestHandler<GetSubscriberByLinkQuery, IEnumerable<SubscriberReadDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubscriberByLinkHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
        public async Task<IEnumerable<SubscriberReadDto>> Handle(GetSubscriberByLinkQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Subscribers
                .Include(s => s.User)
                .Where(s => s.User.LandingPageUrl == request.Link && s.IsDeleted != true)
                .Select(sub => new SubscriberReadDto
                {
                    Id = sub.Id,
                    SubscriptionId = sub.SubscriptionId,
                    UserId = sub.UserId,
                    MobileNumber = sub.User.MobileNumber,
                    GSTAmount = sub.GSTAmount,
                    TotalAmount = sub.TotalAmount,
                    PaymentMode = sub.PaymentMode,
                    TransactionId = sub.TransactionId,
                    TransactionDate = sub.TransactionDate,
                    isActive = sub.isActive,
                    PremiumTelegramChannel = sub.PremiumTelegramChannel,
                    InvoiceId = sub.InvoiceId,
                    CreatedOn = sub.CreatedOn,
                })
                .ToListAsync(cancellationToken: cancellationToken);

            if (entities == null) return null;
            return entities;
        }
    }
}
