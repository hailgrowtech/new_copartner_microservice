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
                                                join s in _dbContext.Subscribers on u.Id equals s.UserId
                                                where !u.IsDeleted
                                                group s by new { u.Id, u.Name, u.MobileNumber } into userGroup
                                                where userGroup.Count() > 1
                                                select new UserSecondTimePaymentListingDto
                                                {
                                                    UserId = userGroup.Key.Id,
                                                    Date = userGroup.FirstOrDefault().CreatedOn, // Assuming CreatedOn represents subscriber creation date
                                                    Mobile = userGroup.Key.MobileNumber,
                                                    Name = userGroup.Key.Name,
                                                    Payment = userGroup.FirstOrDefault().TotalAmount, // Assuming TotalAmount represents the payment amount
                                                    
                                                }).ToListAsync(cancellationToken);


            // (usersWithSecondPayment == null) return null;
            return usersWithSecondPayment;
        }
    }
}
