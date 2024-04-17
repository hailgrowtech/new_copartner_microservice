using MediatR;
using SignInService.Commands;
using SignInService.Data;
using SignInService.Models;

namespace SignInService.Handlers;
public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public CreateCustomerHandler(SignInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PotentialCustomer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Lead;
        await _dbContext.Leads.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}