using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetRelationshipManagerByUserIdHandler : IRequestHandler<GetRelationshipManagerByUserIdQuery, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetRelationshipManagerByUserIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RelationshipManager> Handle(GetRelationshipManagerByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserType == "RA")
            {
                var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.Experts.Id == request.Id && a.IsDeleted != true)
                    .Include(s => s.Experts).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                return RelationshipManagersList;
            }
            else
            {
                var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.AffiliatePartners.Id == request.Id && a.IsDeleted != true)
                    .Include(s => s.AffiliatePartners).SingleOrDefaultAsync(cancellationToken: cancellationToken);
                return RelationshipManagersList;
            }
        }
    }
}
