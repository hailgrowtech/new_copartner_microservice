using MediatR;
using SignInService.Data;
using SignInService.Models;
using SignInService.Queries;
using Microsoft.EntityFrameworkCore;
using SignInService.Commands;

namespace AuthenticationService.Handlers;
public class GetUserDetailsByIdHandler : IRequestHandler<GetUserDetailsByIdQuery, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public GetUserDetailsByIdHandler(SignInDbContext dbContext) => _dbContext = dbContext;
    public async Task<PotentialCustomer> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Leads.Where(x => x.Id == request.Guid).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}