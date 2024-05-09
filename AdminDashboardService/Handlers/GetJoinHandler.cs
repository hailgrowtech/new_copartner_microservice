using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetJoinHandler : IRequestHandler<GetJoinQuery, IEnumerable<JoinReadDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetJoinHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<JoinReadDto>> Handle(GetJoinQuery request, CancellationToken cancellationToken)
        {




            var joinListing = await (from u in _dbContext.Joins
                                         where !u.IsDeleted
                                         select new JoinReadDto
                                         {
                                             ChannelName = u.ChannelName,
                                             Link = u.Link,
                                             Count = 0
                                             
                                         }).ToListAsync(cancellationToken);


            
            if (joinListing == null) return null;
            return joinListing;
        }
    }
}
