using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Logic;

namespace SubscriptionService.Handlers;

public class CreateSubscriberHandler : IRequestHandler<CreateSubscriberCommand, Subscriber>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly ISubscriberBusinessProcessor _subscriberBusineessProcessor;
    public CreateSubscriberHandler(CoPartnerDbContext dbContext, ISubscriberBusinessProcessor subscriberBusineessProcessor)
    {
        _dbContext = dbContext;
        _subscriberBusineessProcessor= subscriberBusineessProcessor;
    }

    public async Task<Subscriber> Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Subscriber;

        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                // Check for duplicate TransactionId
                if (!string.IsNullOrEmpty(entity.TransactionId))
                {
                    var existingSubscriber = await _dbContext.Subscribers
                        .FirstOrDefaultAsync(s => s.TransactionId == entity.TransactionId, cancellationToken);

                    if (existingSubscriber != null)
                    {
                        throw new InvalidOperationException("A subscriber with the same TransactionId already exists.");
                    }
                }

                // Generate InvoiceId
                var subscription = await _dbContext.Subscriptions
                    .Include(s => s.Experts) // Ensure Experts is included
                    .FirstOrDefaultAsync(s => s.Id == entity.SubscriptionId, cancellationToken);

                if (subscription == null)
                {
                    throw new InvalidOperationException("Subscription not found");
                }

                var expertName = subscription.Experts.Name;
                var expertType = subscription.Experts.ExpertTypeId;

                string ServiceType = null;

                if (subscription.ServiceType == "1")
                {
                    ServiceType = "C";
                }
                else if (subscription.ServiceType == "2")
                {
                    ServiceType = "E";
                }
                else if (subscription.ServiceType == "3")
                {
                    ServiceType = "F";
                }

                var prefix = "CP" + expertName.Substring(0, 3).ToUpper() + ServiceType;

                var lastSubscriber = await _dbContext.Subscribers
                    .OrderByDescending(s => s.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                var lastInvoiceNumber = lastSubscriber?.InvoiceId?.Substring(5); // Adjusted to match the prefix length
                var newInvoiceNumber = (int.TryParse(lastInvoiceNumber, out int result) ? result + 1 : 100).ToString("D3");

                entity.InvoiceId = $"{prefix}{newInvoiceNumber}";

                // Ensure the InvoiceId is unique
                while (await _dbContext.Subscribers.AnyAsync(s => s.InvoiceId == entity.InvoiceId, cancellationToken))
                {
                    newInvoiceNumber = (int.Parse(newInvoiceNumber) + 1).ToString("D3");
                    entity.InvoiceId = $"{prefix}{newInvoiceNumber}";
                }

                await _dbContext.Subscribers.AddAsync(entity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // Process subscriber in Wallet and save transaction
                // _subscriberBusinessProcessor.ProcessSubscriberWallet(entity.Id); // Later need to do via RabbitMQ

                request.Subscriber.Id = entity.Id;

                await transaction.CommitAsync(cancellationToken);

                return request.Subscriber;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

}
