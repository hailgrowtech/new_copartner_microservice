using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Data;
using ExpertService.Models;
using ExpertService.Queries;

namespace ExpertService.Handlers;
public class GetExpertsByMobileEmailHandler : IRequestHandler<GetExpertsByMobileNumberOrEmailQuery, Experts>
{
    private readonly ExpertsDbContext _dbContext;
    public GetExpertsByMobileEmailHandler(ExpertsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Experts> Handle(GetExpertsByMobileNumberOrEmailQuery request, CancellationToken cancellationToken)
    {
        var experts = await _dbContext.Experts.Where(x => x.Email == request.Experts.Email || x.MobileNumber == request.Experts.MobileNumber).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return experts;
    }
}