using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetJoinByIdHandler : IRequestHandler<GetJoinByIdQuery, Join>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetJoinByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Join> Handle(GetJoinByIdQuery request, CancellationToken cancellationToken)
        {
            var joinList = await _dbContext.Joins.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return joinList;
        }
    }
}
