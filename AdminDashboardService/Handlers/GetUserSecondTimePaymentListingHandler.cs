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

            var usersWithSecondPayment = await _dbContext.Users
                   .Where(u => !u.IsDeleted)
                   .Join(_dbContext.Subscribers,
                       u => u.Id,
                       sub => sub.UserId,
                       (u, sub) => new { User = u, Subscriber = sub })
                   .GroupBy(join => join.User, join => join.Subscriber, (user, subscribers) => new
                   {
                       User = user,
                       Subscribers = subscribers.ToList() // Materialize subscribers into a list
                   })
                   .Where(group => group.Subscribers.Count > 1) // Filter for users with exactly one subscriber (one payment)
                   .Select(group => new UserSecondTimePaymentListingDto
                   {
                       Date = group.Subscribers.FirstOrDefault().CreatedOn, // Assuming CreatedOn represents subscriber creation date
                       Mobile = group.User.MobileNumber,
                       Name = group.User.Name,
                       Payment = group.Subscribers.FirstOrDefault().TotalAmount // Assuming TotalAmount represents the payment amount
                   })
                   .ToListAsync();

           // (usersWithSecondPayment == null) return null;
            return usersWithSecondPayment;
        }
    }
}
