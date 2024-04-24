using AffiliatePartnerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;

namespace ExpertService.Handlers;
public class GetExpertsByMobileEmailHandler : IRequestHandler<GetAffiliatePartnerMobileNumberOrEmailQuery, AffiliatePartner>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetExpertsByMobileEmailHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AffiliatePartner> Handle(GetAffiliatePartnerMobileNumberOrEmailQuery request, CancellationToken cancellationToken)
    {
        var experts = await _dbContext.AffiliatePartners.Where(x => x.Email == request.AffiliatePartner.Email || x.MobileNumber == request.AffiliatePartner.MobileNumber && x.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return experts;
    } 
}