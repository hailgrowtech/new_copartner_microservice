using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Profiles;

namespace SubscriptionService.Handlers
{
    public class PatchSubscriberHandler : IRequestHandler<PatchSubscriberCommand, Subscriber>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchSubscriberHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<Subscriber> Handle(PatchSubscriberCommand command, CancellationToken cancellationToken)
        {
            var SubscriberToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, command.Subscriber);
            _dbContext.Update(SubscriberToUpdate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return SubscriberToUpdate;
        }
    }
}
