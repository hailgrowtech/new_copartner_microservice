﻿using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class PutRelationshipManagerHandler : IRequestHandler<PutRelationshipManagerCommand, RelationshipManager>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutRelationshipManagerHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RelationshipManager> Handle(PutRelationshipManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = request.RelationshipManager;

            var existingEntity = await _dbContext.RelationshipManagers.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }
    }
}


