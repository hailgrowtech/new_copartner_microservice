using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetExpertAvailabilityByIdHandler : IRequestHandler<GetExpertAvailabilityByIdQuery, ExpertAvailability>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetExpertAvailabilityByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExpertAvailability> Handle(GetExpertAvailabilityByIdQuery request, CancellationToken cancellationToken)
        {
            var webinar = await _dbContext.ExpertAvailabilities.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return webinar;
        }
    }
}
