using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class CreateRelationshipManagerHandler : IRequestHandler<CreateRelationshipMangerCommand, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateRelationshipManagerHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RelationshipManager> Handle(CreateRelationshipMangerCommand request, CancellationToken cancellationToken)
        {
            var entity = request.RelationshipManager;
            await _dbContext.RelationshipManagers.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.RelationshipManager.Id = entity.Id;
            return request.RelationshipManager;
        }
    }
}
