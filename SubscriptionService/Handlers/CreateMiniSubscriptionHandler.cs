using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Logic;
using System.Text.RegularExpressions;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SubscriptionService.Handlers
{
    public class CreateMiniSubscriptionHandler : IRequestHandler<CreateMiniSubscriptionLinkCommand, MinisubscriptionLink>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly ISubscriberBusinessProcessor _subscriberBusineessProcessor;
        public CreateMiniSubscriptionHandler(CoPartnerDbContext dbContext, ISubscriberBusinessProcessor subscriberBusineessProcessor)
        {
            _dbContext = dbContext;
            _subscriberBusineessProcessor = subscriberBusineessProcessor;
        }

        public async Task<MinisubscriptionLink> Handle(CreateMiniSubscriptionLinkCommand request, CancellationToken cancellationToken)
        {
            var expert = await _dbContext.Experts.Where(x => x.Id == request.ExpertsId).FirstOrDefaultAsync();
            var subscription = await _dbContext.Subscriptions.Where(x => x.Id == request.SubscriptionId).FirstOrDefaultAsync();

            var existingMiniSubscriptionLink = await _dbContext.MinisubscriptionLink.Where(x => x.ExpertId == request.ExpertsId && x.SubscriptionId == request.SubscriptionId).FirstOrDefaultAsync();



            if (expert == null)
            {
                return null;
            }
            else if (subscription == null)
            {
                return null;
            }
            else if (existingMiniSubscriptionLink != null)
            {
                return null;
            }
            else
            {
                var baseUri = "https://copartner.in/ra-detail2";
                var referralLink = $"{baseUri}/{request.SubscriptionId}";

                var links = new MinisubscriptionLink();

                var generatedLink = new MinisubscriptionLink
                {
                    ExpertId = request.ExpertsId,
                    SubscriptionId = request.SubscriptionId,
                    MiniSubscriptionLink = referralLink
                };


                await _dbContext.MinisubscriptionLink.AddAsync(generatedLink, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return generatedLink;
            }
        }
    }
}
