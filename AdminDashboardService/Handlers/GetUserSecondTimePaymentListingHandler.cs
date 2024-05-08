using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;

namespace AdminDashboardService.Handlers
{
    public class GetUserSecondTimePaymentListingHandler : IRequestHandler<GetUserSecondTimePaymentListingQuery, IEnumerable<UserSecondTimePaymentListingDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetUserSecondTimePaymentListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<UserSecondTimePaymentListingDto>> Handle(GetUserSecondTimePaymentListingQuery request, CancellationToken cancellationToken)
        {

            var usersWithSecondPayment = await (from u in _dbContext.Users
                                                join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
                                                from sub in gj.DefaultIfEmpty()
                                                where sub != null // Only include users with subscription records
                                                group sub by u into userGroup
                                                where userGroup.Count() > 1 // Filter users with more than one payment
                                                select new UserSecondTimePaymentListingDto
                                                {
                                                    Date = userGroup.FirstOrDefault().CreatedOn,
                                                    Mobile = userGroup.Key.MobileNumber,
                                                    Name = userGroup.Key.Name,
                                                    Payment = userGroup.FirstOrDefault().TotalAmount
                                                }).ToListAsync(cancellationToken);

            return usersWithSecondPayment;
        }
    }
}
