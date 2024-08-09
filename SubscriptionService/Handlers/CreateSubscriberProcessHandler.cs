using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using Razorpay.Api;
using SubscriptionService.Commands;
using SubscriptionService.Logic;

namespace SubscriptionService.Handlers;

public class CreateSubscriberProcessHandler : IRequestHandler<CreateSubscriberProcessCommand, Subscriber>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly ISubscriberBusinessProcessor _subscriberBusineessProcessor;
    public CreateSubscriberProcessHandler(CoPartnerDbContext dbContext, ISubscriberBusinessProcessor subscriberBusineessProcessor)
    {
        _dbContext = dbContext;
        _subscriberBusineessProcessor= subscriberBusineessProcessor;
    }
    public async Task<Subscriber> Handle(CreateSubscriberProcessCommand request, CancellationToken cancellationToken)
    {
            // Ensure that the request parameters are valid
            if (request == null || request.UserId == Guid.Empty || request.SubscriberId == Guid.Empty)
            {
                throw new ArgumentException("Invalid request parameters.");
            }

            // Step 1: Update TempSubscriber records
            var tempSubscribers = await _dbContext.TempSubscribers
                .Where(ts => ts.Id == request.SubscriberId && ts.IsProcessed != true)
                .ToListAsync(cancellationToken);

            if (tempSubscribers.Any())
            {
                // Update TempSubscribers
                foreach (var tempSubscriber in tempSubscribers)
                {
                    tempSubscriber.IsProcessed = true;
                }

                // Save changes for TempSubscribers update
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            // Step 2: Insert records into Subscriber table
            var newSubscribers = tempSubscribers.Select(ts => new Subscriber
            {
                Id = ts.Id,
                SubscriptionId = ts.SubscriptionId,
                UserId = request.UserId,
                GSTAmount = ts.GSTAmount,
                TotalAmount = ts.TotalAmount,
                DiscountPercentage = ts.DiscountPercentage,
                PaymentMode = ts.PaymentMode,
                TransactionId = ts.TransactionId,
                TransactionDate = ts.TransactionDate,
                isActive = ts.isActive,
                PremiumTelegramChannel = ts.PremiumTelegramChannel,
                InvoiceId = ts.InvoiceId,
                CreatedBy = ts.CreatedBy,
                CreatedOn = ts.CreatedOn,
                UpdatedBy = ts.UpdatedBy,
                UpdatedOn = ts.UpdatedOn,
                IsDeleted = ts.IsDeleted,
                DeletedBy = ts.DeletedBy,
                DeletedOn = ts.DeletedOn
            }).ToList();

            if (newSubscribers.Any())
            {
                // Add new Subscribers to the context
                _dbContext.Subscribers.AddRange(newSubscribers);

                // Save changes for Subscriber insertion
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return newSubscribers.FirstOrDefault();
        }
    }


}
