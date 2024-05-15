using AdminDashboardService.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class CreateJoinHandler : IRequestHandler<CreateJoinCommand, Join>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateJoinHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Join> Handle(CreateJoinCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Join;
            // Check if the ChannelName is unique
            bool isUnique = await IsChannelNameUnique(entity.ChannelName);
            if (!isUnique)
            {
                return null;
            }

            await _dbContext.Joins.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.Join.Id = entity.Id;
            return request.Join;
        }
        private async Task<bool> IsChannelNameUnique(string ChannelName)
        {
            // Normalize input to lowercase
            string lowerCaseChannelName = ChannelName.ToLower();

            // Check if any existing entity has the same Title (case-insensitive)
            var existingEntity = await _dbContext.Joins
                .FirstOrDefaultAsync(a => a.ChannelName.ToLower() == lowerCaseChannelName);

            // Return true if no existing entity is found, indicating uniqueness
            return existingEntity == null;
        }
    }
}
