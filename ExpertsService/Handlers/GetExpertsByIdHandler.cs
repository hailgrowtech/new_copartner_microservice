using MediatR;
using Microsoft.EntityFrameworkCore;


using ExpertService.Queries;
using MigrationDB.Models;
using MigrationDB.Data;

namespace ExpertService.Handlers;

public class GetExpertsByIdHandler : IRequestHandler<GetExpertsByIdQuery, Experts>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetExpertsByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Experts> Handle(GetExpertsByIdQuery request, CancellationToken cancellationToken)
    {
        var expertsList = await _dbContext.Experts.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return expertsList;
    }
}