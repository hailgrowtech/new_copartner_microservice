using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using CommonLibrary.Extensions;

namespace AdminDashboardService.Handlers;
public class GetUserFirstTimePaymentListingHandler : IRequestHandler<GetUserFirstTimePaymentListingQuery, IEnumerable<UserFirstTimePaymentListingDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUserFirstTimePaymentListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
    public async Task<IEnumerable<UserFirstTimePaymentListingDto>> Handle(GetUserFirstTimePaymentListingQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;

        var usersWithFirstPayment = await (from u in _dbContext.Users
                                           where !u.IsDeleted
                                           join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
                                           from sub in gj.DefaultIfEmpty()
                                           where sub == null || !sub.IsDeleted
                                           group sub by u into userGroup
                                           where userGroup.Count() == 1 && userGroup.All(sub => sub != null)
                                           select new UserFirstTimePaymentListingDto
                                           {
                                               UserId = userGroup.Key.Id,
                                               Date = userGroup.FirstOrDefault().CreatedOn.ToIST(), // Assuming CreatedOn represents subscriber creation date
                                               Mobile = userGroup.Key.MobileNumber,
                                               Name = userGroup.Key.Name,
                                               Payment = userGroup.FirstOrDefault().TotalAmount, // Assuming TotalAmount represents the payment amount
                                               APId = userGroup.Key.AffiliatePartnerId,
                                               RAId = userGroup.Key.ExpertsID
                                               //PaymentRAId = 
                                               //PaymentRAName = 
                                           }).Skip(skip)
                                            .Take(request.PageSize)
                                            .ToListAsync(cancellationToken);


        if (usersWithFirstPayment == null) return null;
        return usersWithFirstPayment;
    }
}
