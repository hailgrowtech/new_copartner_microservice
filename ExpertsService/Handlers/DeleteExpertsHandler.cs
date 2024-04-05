using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Commands;
using ExpertService.Data;
using ExpertService.Models;

namespace ExpertService.Handlers;
public class DeleteExpertsHandler : IRequestHandler<DeleteExpertsCommand, Experts>
{
    private readonly ExpertsDbContext _dbContext;
    public DeleteExpertsHandler(ExpertsDbContext dbContext) => _dbContext = dbContext;
    public async Task<Experts> Handle(DeleteExpertsCommand request, CancellationToken cancellationToken)
    {
        var expertsList = await _dbContext.Experts.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (expertsList == null) return null;
        _dbContext.Experts.Remove(expertsList);
        await _dbContext.SaveChangesAsync();
        return expertsList;
    }
}
