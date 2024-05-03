using AdminDashboardService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class PutBlogHandler : IRequestHandler<PutBlogCommand, Blog>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutBlogHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Blog> Handle(PutBlogCommand request, CancellationToken cancellationToken)
        {
            var entity = request.blog;

            var existingEntity = await _dbContext.Blogs.FindAsync(entity.Id);
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
