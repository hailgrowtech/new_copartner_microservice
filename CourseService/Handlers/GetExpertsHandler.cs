using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Data;
using ExpertService.Models;
using ExpertService.Queries;

namespace ExpertService.Handlers;
public class GetExpertsHandler : IRequestHandler<GetExpertsQuery, IEnumerable<Experts>>
{
    private readonly ExpertsDbContext _dbContext;
    public GetExpertsHandler(ExpertsDbContext dbContext) => _dbContext = dbContext;
    public async Task<IEnumerable<Experts>> Handle(GetExpertsQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.Experts.ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}