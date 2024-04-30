using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;
using MigrationDB.Data;

namespace ExpertsService.Handlers
{
    public class GetRAListingDetailsHandler : IRequestHandler<GetRAListingDetailsQuery, IEnumerable<RAListingDetailsDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetRAListingDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<RAListingDetailsDto>> Handle(GetRAListingDetailsQuery request, CancellationToken cancellationToken)
        {

            var ralisting = new RAListingDetailsDto
            {
                JoinDate = _dbContext.Experts.Select(a => a.CreatedOn).SingleOrDefault(),
                Name = _dbContext.Experts.Select(a => a.Name).SingleOrDefault(),
                SEBINo = _dbContext.Experts.Select(a => a.SEBIRegNo).SingleOrDefault(),
                FixCommission = _dbContext.Experts.Select(a => a.FixCommission).SingleOrDefault(),
                RAEarning = 0
            };

            //if (aplisting == null) return null;
            return new List<RAListingDetailsDto> { ralisting };
        }

    }
}
