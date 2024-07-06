using ExpertService.Commands;
using ExpertsService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class CreateExpertAvailabilityHandler :  IRequestHandler<CreateExpertAvailabilityCommand, ExpertAvailability>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateExpertAvailabilityHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExpertAvailability> Handle(CreateExpertAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ExpertAvailability;
            await _dbContext.ExpertAvailabilities.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.ExpertAvailability.Id = entity.Id;
            //request.Experts.isActive = entity.isActive;
            return request.ExpertAvailability;
        }
    }
}
