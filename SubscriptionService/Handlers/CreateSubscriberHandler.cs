using MediatR;
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
        await _dbContext.Subscribers.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        // Process subscriber in Wallet and save transaction
       // _subscriberBusineessProcessor.ProcessSubscriberWallet(entity.Id); // Later  need to do via RabbitMQ
        request.Subscriber.Id = entity.Id;
        return request.Subscriber;
    }
}
