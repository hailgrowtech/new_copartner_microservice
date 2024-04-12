using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;

namespace SubscriptionService.Handlers;

public class PutSubscriptionHandler : IRequestHandler<PutSubscriptionCommand, Subscription>
{
    private readonly CoPartnerDbContext _dbContext;
    public PutSubscriptionHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subscription> Handle(PutSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Subscription;

        var existingEntity = await _dbContext.Subscriptions.FindAsync(entity.Id);
        if (existingEntity == null)
        {
            return null; // or throw an exception indicating the entity not found
        }

        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity; // Return the updated entity
    }
}
