using ExpertService.Queries;
using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetExpertAvailabilityHandler : IRequestHandler<GetExpertAvailabilityQuery, IEnumerable<ExpertAvailability>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetExpertAvailabilityHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<ExpertAvailability>> Handle(GetExpertAvailabilityQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;

            var entities = await _dbContext.ExpertAvailabilities.Where(x => x.IsDeleted != true)
                            //.OrderByDescending(x => x.CreatedOn)
                            .Skip(skip)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;

        }
    }
}
