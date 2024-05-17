using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
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

            var userDatalisting = new UserDataListingDto
            {
                Date = DateTime.Now,
                Mobile = "32423",
                Name = "sample"
          
            };

            //if (aplisting == null) return null;
            return new List<UserDataListingDto> { userDatalisting };
        }
    }
}
