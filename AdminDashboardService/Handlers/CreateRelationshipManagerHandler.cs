using AdminDashboardService.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            // Check if the combination of AgencyName and Link is unique
            bool isUnique = await IsDataUnique(entity.Mobile, entity.Email);
            if (!isUnique)
            {
                return null;
            }
            await _dbContext.RelationshipManagers.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.RelationshipManager.Id = entity.Id;
            return request.RelationshipManager;
        }
        private async Task<bool> IsDataUnique(string mobile, string email)
        {
            // Normalize input to lowercase
            string lowerCaseMobile = mobile.ToLower();
            string lowerCaseEmail = email.ToLower();

            // Check if any existing entity has the same AgencyName and Link combination (case-insensitive)
            var existingEntity = await _dbContext.RelationshipManagers
                .FirstOrDefaultAsync(a =>  a.Mobile.ToLower() == lowerCaseMobile && a.Mobile.ToLower() == lowerCaseMobile);

            // Return true if no existing entity is found, indicating uniqueness
            return existingEntity == null;
        }
    }
}
