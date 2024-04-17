using MediatR;
using SignInService.Commands;
using SignInService.Data;
using SignInService.Models;

namespace SignInService.Handlers;
public class ValidateCustomerHandler : IRequestHandler<ValidateCustomerCommand, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public ValidateCustomerHandler(SignInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PotentialCustomer> Handle(ValidateCustomerCommand request, CancellationToken cancellationToken)
    {
        PotentialCustomer lead = new PotentialCustomer();
        if (!request.isLeadUpdateRequest)
        {
            return _dbContext.Leads.Where(a => a.MobileNumber == request.Lead.MobileNumber).OrderByDescending(x => x.OtpExpiryTime).FirstOrDefault();
        }
        else
        {
            lead = request.Lead;
            lead.IsOTPValidated = true;

            _dbContext.Leads.Attach(lead);
            _dbContext.Entry(lead).Property(x => x.IsOTPValidated).IsModified = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return lead;
        }

    }
}