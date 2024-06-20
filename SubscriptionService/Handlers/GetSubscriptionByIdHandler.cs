using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Queries;

namespace SubscriptionService.Handlers;

public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, Subscription>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetSubscriptionByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subscription> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _dbContext.Subscriptions
            .Include(s => s.Experts)
            .Where(a => a.Id == request.Id && !a.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (subscription != null)
        {
            subscription.ConvertAllDateTimesToIST(); // Assuming this method modifies the subscription object
        }

        return subscription;
    }
}
