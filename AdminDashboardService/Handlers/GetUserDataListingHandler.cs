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

        public GetUserDataListingHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDataListingDto>> Handle(GetUserDataListingQuery request, CancellationToken cancellationToken)
        {

            var userDataListing = await (from u in _dbContext.Users
                                         where !u.IsDeleted
                                         join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
                                         from sub in gj.DefaultIfEmpty()
                                         where sub == null
                                         select new UserDataListingDto
                                         {
                                             Date = u.CreatedOn, // Replace with appropriate value
                                             Mobile = u.MobileNumber,
                                             Name = u.Name
                                         }).ToListAsync(cancellationToken);

            //if (aplisting == null) return null;
            return userDataListing;
        }
    }
}
