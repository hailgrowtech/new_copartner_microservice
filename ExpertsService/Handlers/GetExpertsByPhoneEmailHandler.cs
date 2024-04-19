using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Queries;
using MigrationDB.Data;
using MigrationDB.Models;

namespace ExpertService.Handlers;
public class GetExpertsByMobileEmailHandler : IRequestHandler<GetExpertsByMobileNumberOrEmailQuery, Experts>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetExpertsByMobileEmailHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Experts> Handle(GetExpertsByMobileNumberOrEmailQuery request, CancellationToken cancellationToken)
    {
        var experts = await _dbContext.Experts.Where(x => x.Email == request.Experts.Email || x.MobileNumber == request.Experts.MobileNumber && x.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return experts;
    }
}