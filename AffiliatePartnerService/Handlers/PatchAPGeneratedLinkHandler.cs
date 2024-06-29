using AffiliatePartnerService.Commands;
using AffiliatePartnerService.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using CommonLibrary.CommonModels;

namespace AffiliatePartnerService.Handlers
{
    public class PatchAPGeneratedLinkHandler : IRequestHandler<PatchAPGeneratedLinkCommand, APGeneratedLinks>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;
        public PatchAPGeneratedLinkHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<APGeneratedLinks> Handle(PatchAPGeneratedLinkCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentAPGeneratedLink = await _dbContext.APGeneratedLinks.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentAPGeneratedLink == null)
            {
                // Handle the case where the subscriber does not exist
                throw new Exception($"AP Generated Link with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var APGeneratedLinkToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentAPGeneratedLink);
            APGeneratedLinkToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.APGeneratedLinks.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(APGeneratedLinkToUpdate);
            _dbContext.Entry(APGeneratedLinkToUpdate).State = EntityState.Modified;

            // Preserve multiple properties 
            _dbContext.PreserveProperties(trackedEntity, currentAPGeneratedLink, "CreatedOn");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return APGeneratedLinkToUpdate;
        }
    }
}
