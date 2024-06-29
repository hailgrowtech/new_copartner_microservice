using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Profiles;
using CommonLibrary.CommonModels;

namespace SubscriptionService.Handlers;
public class PatchPaymentResponseHandler : IRequestHandler<PatchPaymentResponseCommand, PaymentResponse>
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IJsonMapper _jsonMapper;
    public PatchPaymentResponseHandler(CoPartnerDbContext dbContext, IJsonMapper jsonMapper)
    {
        _dbContext = dbContext;
        _jsonMapper = jsonMapper;
    }
    public async Task<PaymentResponse> Handle(PatchPaymentResponseCommand command, CancellationToken cancellationToken)
    {
        // Fetch the current entity from the database without tracking it
        var currentPayment = await _dbContext.PaymentResponses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (currentPayment == null)
        {
            // Handle the case where the subscriber does not exist
            throw new Exception($"Subscriber with ID {command.Id} not found.");
        }

        // Apply the patch to the existing entity
        var PaymentToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentPayment);
        PaymentToUpdate.Id = command.Id;

        // Detach any existing tracked entity with the same ID
        var trackedEntity = _dbContext.PaymentResponses.Local.FirstOrDefault(e => e.Id == command.Id);
        if (trackedEntity != null)
        {
            _dbContext.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the updated entity and mark it as modified
        _dbContext.Attach(PaymentToUpdate);
        _dbContext.Entry(PaymentToUpdate).State = EntityState.Modified;

        // Preserve multiple properties 
        _dbContext.PreserveProperties(PaymentToUpdate, currentPayment, "CreatedOn");

        await _dbContext.SaveChangesAsync(cancellationToken);

        return PaymentToUpdate;
    }
}
