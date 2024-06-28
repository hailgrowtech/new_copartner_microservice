using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using CommonLibrary.Extensions;

namespace AdminDashboardService.Handlers;

public class GetUserDataListingHandler : IRequestHandler<GetUserDataListingQuery, IEnumerable<UserDataListingDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUserDataListingHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<UserDataListingDto>> Handle(GetUserDataListingQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;

        var result = await (from u in _dbContext.Users
         where !u.IsDeleted
         join s in _dbContext.Subscribers on u.Id equals s.UserId into gj
         from sub in gj.DefaultIfEmpty()
         where sub == null
         select new UserDataListingDto
         {
             UserId = u.Id,
             Date = u.CreatedOn.ToIST(), // Convert the date to IST, // Assuming CreatedOn is the registration date
             Name = u.Name,
             Mobile = u.MobileNumber,
             APId = u.AffiliatePartnerId,
             ExpertId = u.ExpertsID
             
         })
            //.OrderByDescending(x => x.Date)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        if (result == null) return null;
        return result;

    }
}
