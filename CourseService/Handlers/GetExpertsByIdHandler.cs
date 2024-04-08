using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Data;
using ExpertService.Models;
using ExpertService.Queries;

namespace ExpertService.Handlers;
public class GetExpertsByIdHandler : IRequestHandler<GetExpertsByIdQuery, Experts>
{
    private readonly ExpertsDbContext _dbContext;
    public GetExpertsByIdHandler(ExpertsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Experts> Handle(GetExpertsByIdQuery request, CancellationToken cancellationToken)
    {
        var expertsList = await _dbContext.Experts.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return expertsList;
    }
}