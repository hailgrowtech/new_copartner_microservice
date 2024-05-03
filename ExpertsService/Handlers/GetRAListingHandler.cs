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
            var expert = await _dbContext.Experts
                .Where(exp => !exp.IsDeleted)
                .Select(exp => new { Name = exp.Name })
                .FirstOrDefaultAsync();

            var userCount = await _dbContext.Users
                .CountAsync(u => u.ReferralMode == "RA" && !u.IsDeleted);

            var subscriberIdQuery = from sub in _dbContext.Subscribers
                                    where !sub.IsDeleted
                                    join wallet in _dbContext.Wallets on sub.Id equals wallet.SubscriberId into walletGroup
                                    select sub.Id;

            var subscriberId = await subscriberIdQuery.FirstOrDefaultAsync();

            var earnings = await _dbContext.Wallets
                .Where(w => !w.IsDeleted)
                .GroupBy(w => 1) // Grouping by a constant to get total sum
                .Select(g => new
                {
                    RAAmount = g.Sum(w => w.RAAmount),
                    CPAmount = g.Sum(w => w.CPAmount)
                })
                .FirstOrDefaultAsync();

            var result = new RAListingDto
            {
                Name = expert != null ? expert.Name : null,
                UsersCount = userCount,
                RAEarning = earnings != null ? earnings.RAAmount : 0,
                CPEarning = earnings != null ? earnings.CPAmount : 0
            };

            return new List<RAListingDto> { result };
        }

    }
}
