using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using CommonLibrary.Extensions;

namespace AdminDashboardService.Handlers;
public class GetUserSecondTimePaymentListingHandler : IRequestHandler<GetUserSecondTimePaymentListingQuery, IEnumerable<UserSecondTimePaymentListingDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUserSecondTimePaymentListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<UserSecondTimePaymentListingDto>> Handle(GetUserSecondTimePaymentListingQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;


        var userData = await (from u in _dbContext.Users
                              join s in _dbContext.Subscribers on u.Id equals s.UserId
                              where !u.IsDeleted && !s.IsDeleted
                              select new { u, s })
                         .ToListAsync(cancellationToken);

        var usersWithSecondPayment = userData.GroupBy(x => x.u.Id)
                                             .Where(g => g.Count() > 1)
                                             .SelectMany(g => g.Select(sub => new UserSecondTimePaymentListingDto
                                             {
                                                 UserId = sub.u.Id,
                                                 Date = sub.s.CreatedOn.ToIST(),
                                                 Mobile = sub.u.MobileNumber,
                                                 Name = sub.u.Name,
                                                 Payment = sub.s.TotalAmount,
                                                 ReferralMode = sub.u.ReferralMode,
                                                 APId = sub.u.AffiliatePartnerId,
                                                 RAId = sub.u.ExpertsID,
                                                 RASubscriber = sub.s.Subscription != null ? sub.s.Subscription.ExpertsId : (Guid?)null
                                             }))
                                             //.OrderByDescending(x => x.Date)
                                             .Skip(skip)
                                             .Take(request.PageSize)
                                             .ToList();





        if (usersWithSecondPayment == null) return null;
        return usersWithSecondPayment;
    }
}
