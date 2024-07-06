using ExpertService.Profiles;
using ExpertsService.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using CommonLibrary.CommonModels;

namespace ExpertsService.Handlers
{
    public class PatchExpertAvailabilityHandler : IRequestHandler<PatchExpertAvailabilityCommand, ExpertAvailability>
    {
        private readonly CoPartnerDbContext _dbContext;
        private readonly IJsonMapper _jsonMapper;

        public PatchExpertAvailabilityHandler(CoPartnerDbContext dbContext,
                                IJsonMapper jsonMapper)
        {
            _dbContext = dbContext;
            _jsonMapper = jsonMapper;
        }

        public async Task<ExpertAvailability> Handle(PatchExpertAvailabilityCommand command, CancellationToken cancellationToken)
        {
            // Fetch the current entity from the database without tracking it
            var currentExpert = await _dbContext.ExpertAvailabilities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

            if (currentExpert == null)
            {
                // Handle the case where the expert does not exist
                throw new Exception($"Expert Availability with ID {command.Id} not found.");
            }

            // Apply the patch to the existing entity
            var expertsToUpdate = _jsonMapper.ToDomain(command.JsonPatchDocument, currentExpert);
            expertsToUpdate.Id = command.Id;

            // Detach any existing tracked entity with the same ID
            var trackedEntity = _dbContext.ExpertAvailabilities.Local.FirstOrDefault(e => e.Id == command.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            // Attach the updated entity and mark it as modified
            _dbContext.Attach(expertsToUpdate);
            _dbContext.Entry(expertsToUpdate).State = EntityState.Modified;

            // Preserve multiple properties 
            _dbContext.PreserveProperties(expertsToUpdate, currentExpert, "CreatedOn");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return expertsToUpdate;
        }
    }
}
