using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetRelationshipManagerByIdHandler : IRequestHandler<GetRelationshipManagerByIdQuery, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetRelationshipManagerByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RelationshipManager> Handle(GetRelationshipManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return RelationshipManagersList;
        }
    }
}
