using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetRelationshipManagerByUserIdHandler : IRequestHandler<GetRelationshipManagerByUserIdQuery, IEnumerable<RelationshipManager>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetRelationshipManagerByUserIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RelationshipManager>> Handle(GetRelationshipManagerByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserType == "RA")
            {
                if (request.Id.ToString().Length > 10)
                {
                    var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.Experts.Id == request.Id && a.IsDeleted != true)
                        .Include(s => s.Experts)
                        .OrderByDescending(x => x.CreatedOn)
                        .ToListAsync(cancellationToken: cancellationToken);
                    return RelationshipManagersList;
                }
                else
                {
                    var RelationshipManagersList = await _dbContext.RelationshipManagers
                        .Include(s => s.Experts)
                        .OrderByDescending(x => x.CreatedOn)
                        .ToListAsync(cancellationToken: cancellationToken);
                    return RelationshipManagersList;
                }
            }
            else
            {
                if (request.Id.ToString().Length > 10)
                {
                    var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.AffiliatePartners.Id == request.Id && a.IsDeleted != true)
                    .Include(s => s.AffiliatePartners)
                    .OrderByDescending(x => x.CreatedOn)
                    .ToListAsync(cancellationToken: cancellationToken);
                    return RelationshipManagersList;
                }
                else
                {
                    var RelationshipManagersList = await _dbContext.RelationshipManagers
                    .Include(s => s.AffiliatePartners)
                    .OrderByDescending(x => x.CreatedOn)
                    .ToListAsync(cancellationToken: cancellationToken);
                    return RelationshipManagersList;
                }
            }
        }
    }
}
