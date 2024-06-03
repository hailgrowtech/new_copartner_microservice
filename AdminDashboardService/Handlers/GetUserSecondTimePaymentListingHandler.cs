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
            int skip = (request.Page - 1) * request.PageSize;



            var userData = await (from u in _dbContext.Users
                                  join s in _dbContext.Subscribers on u.Id equals s.UserId
                                  where !u.IsDeleted
                                  select new { u, s })
                     .ToListAsync(cancellationToken);

            var usersWithSecondPayment = userData.GroupBy(x => x.u.Id)
                                                 .Where(g => g.Count() > 1)
                                                 .SelectMany(g => g.Select(sub => new UserSecondTimePaymentListingDto
                                                 {
                                                     UserId = sub.u.Id,
                                                     Date = sub.s.CreatedOn,
                                                     Mobile = sub.u.MobileNumber,
                                                     Name = sub.u.Name,
                                                     Payment = sub.s.TotalAmount
                                                 }))
                                                 .Skip(skip)
                                                 .Take(request.PageSize);




            if (usersWithSecondPayment == null) return null;
            return usersWithSecondPayment;
        }
    }
}
