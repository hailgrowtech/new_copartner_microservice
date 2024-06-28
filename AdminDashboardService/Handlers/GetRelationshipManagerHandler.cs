using AdminDashboardService.Commands;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{

    public class GetRelationshipManagerHandler : IRequestHandler<GetRelationshipManagerQuery, IEnumerable<RelationshipManager>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetRelationshipManagerHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<RelationshipManager>> Handle(GetRelationshipManagerQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.RelationshipManagers.Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken: cancellationToken);
            if (entities == null) return null;
            return entities;
        }
    }
}


