using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace ExpertsService.Handlers
{
    public class GetRAListingHandler : IRequestHandler<GetRAListingQuery, IEnumerable<RAListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetRAListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<RAListingDto>> Handle(GetRAListingQuery request, CancellationToken cancellationToken)
        {

            var aplisting = new RAListingDto
            {
                Name = _dbContext.Experts.Select(a => a.Name).SingleOrDefault(),
                UsersCount = _dbContext.Users.Select(a => a.ReferralMode=="RA" && a.IsDeleted!=true).Count(),
                RAEarning = 0,
                CPEarning = 0,
            };

            //if (aplisting == null) return null;
            return new List<RAListingDto> { aplisting };
        }
    }
}
