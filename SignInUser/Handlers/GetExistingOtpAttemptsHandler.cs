using MediatR;
using SignInService.Data;
using SignInService.Models;
using SignInService.Queries;

namespace SignInService.Handlers;
public class GetExistingOtpAttemptsHandler : IRequestHandler<GetExistingOtpAttemptsQuery, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public GetExistingOtpAttemptsHandler(SignInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PotentialCustomer> Handle(GetExistingOtpAttemptsQuery request, CancellationToken cancellationToken)
    {
        var lead = _dbContext.Leads.Where(a => a.MobileNumber == request.Lead.MobileNumber || a.PublicIP == request.Lead.PublicIP).OrderByDescending(x => x.OtpExpiryTime).FirstOrDefault();
        return null;
    }
}