using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using System.Linq;
using System.Xml.Linq;

namespace AdminDashboardService.Handlers
{
    public class GetAPTransactionDetailsHandler : IRequestHandler<GetAPTransactionsDetailsQuery, IEnumerable<APTransactionsDetailsReadDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetAPTransactionDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<APTransactionsDetailsReadDto>> Handle(GetAPTransactionsDetailsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Blogs.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);


            var APTransactionsDetailsReadDtoList = new APTransactionsDetailsReadDto
            {
                Date = _dbContext.Withdrawals.Select(x=> x.CreatedOn).FirstOrDefault(),
                APName = _dbContext.Withdrawals.Select(x=> x.WithdrawalBy).FirstOrDefault(),
                //Mobile = _dbContext.AffiliatePartners.Where(y=> y.Name== APName),
                Request = _dbContext.Withdrawals.Select(x => x.isApproved).FirstOrDefault(),
            };


            if (APTransactionsDetailsReadDtoList == null) return null;
            return new List<APTransactionsDetailsReadDto> { APTransactionsDetailsReadDtoList };
        }
    }
}
