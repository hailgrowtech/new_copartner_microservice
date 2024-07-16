using Copartner;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers;

public class GetSubscriberWalletHandler : IRequestHandler<GetSubscriberWalletQuery, WalletEvent>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetSubscriberWalletHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
    public async Task<WalletEvent> Handle(GetSubscriberWalletQuery request, CancellationToken cancellationToken)
    {
        var subscriber = await _dbContext.Subscribers
                .Include(s => s.User)
                .Include(s => s.Subscription)
                .Include(s=> s.Subscription.Experts)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync(s => s.Id == request.Id);

        if (subscriber == null)
        {
            throw new ArgumentException("Subscriber not found.");
        }

        var referralMode = subscriber.User.ReferralMode;
         var userExpertsId = subscriber.User.ExpertsID;
        var expertsId = subscriber.Subscription?.ExpertsId;
        var affiliatePartnerId = subscriber.User.AffiliatePartnerId;
        var amount = subscriber.TotalAmount;
        var isCoPartner = subscriber.Subscription.Experts.isCoPartner;
        var IsSpecialSubscription = subscriber.Subscription.IsSpecialSubscription;

        decimal raAmount = 0, apAmount = 0, cpAmount = 0;
        if(!isCoPartner)
        {
            raAmount = amount;
        }

        else if (referralMode == "RA" && userExpertsId == expertsId)
        {
            raAmount = expertsId != null ? amount : amount * ((decimal)_dbContext.Experts.Find(expertsId).FixCommission) / 100;
        }
        else if (referralMode == "RA" && userExpertsId != expertsId)
        {
            raAmount = amount * ((decimal)_dbContext.Experts.Find(expertsId).FixCommission) / 100;
        }

        else if(referralMode == "AP" && IsSpecialSubscription == true)
        {
            var affiliatePartnerCommission = _dbContext.AffiliatePartners.Find(affiliatePartnerId).FixCommission1;
            apAmount = amount * ((decimal)affiliatePartnerCommission) / 100;
            raAmount = amount * ((decimal)_dbContext.Experts.Find(expertsId).FixCommission) / 100;
        }
        else if (referralMode == "AP" && IsSpecialSubscription == false)
        {
            var affiliatePartnerCommission = (_dbContext.Subscribers.Count(s => s.UserId == subscriber.UserId && s.Subscription.IsSpecialSubscription == false) < 2) ?
                _dbContext.AffiliatePartners.Find(affiliatePartnerId).FixCommission1 :
                _dbContext.AffiliatePartners.Find(affiliatePartnerId).FixCommission2;

            apAmount = amount * ((decimal)affiliatePartnerCommission) / 100;
            raAmount = amount * ((decimal)_dbContext.Experts.Find(expertsId).FixCommission) / 100;
        }
        else
        {
            raAmount = amount * ((decimal)_dbContext.Experts.FirstOrDefault(e => e.Id == expertsId)?.FixCommission) / 100;
        }

        cpAmount = amount - (raAmount + apAmount);

        return InsertTransaction(request.Id, affiliatePartnerId, expertsId, raAmount, apAmount, cpAmount);
    }
    private WalletEvent InsertTransaction(Guid subscriberId, Guid? affiliatePartnerId, Guid? expertsId, decimal raAmount, decimal apAmount, decimal cpAmount)
    {
        var transaction = new WalletEvent
        {
            SubscriberId = subscriberId,
            AffiliatePartnerId = affiliatePartnerId,
            ExpertsId = expertsId,
            RAAmount = raAmount,
            APAmount = apAmount,
            CPAmount = cpAmount,
            TransactionDate = DateTime.Now
        };
        return transaction;
    }
}
