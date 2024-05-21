using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetUserDataListingHandler : IRequestHandler<GetUserDataListingQuery, IEnumerable<UserDataListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetUserDataListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<UserDataListingDto>> Handle(GetUserDataListingQuery request, CancellationToken cancellationToken)
        {

            var result = await (from u in _dbContext.Users
             where !u.IsDeleted
             join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
             from sub in gj.DefaultIfEmpty()
             where sub == null
             select new UserDataListingDto
             {
                 Date = u.CreatedOn, // Assuming CreatedOn is the registration date
                 Name = u.Name,
                 Mobile = u.MobileNumber,
                 
             }).ToListAsync(cancellationToken);

            //if (aplisting == null) return null;
            return result;
        }
    }
}
