using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpertService.Commands;

using MigrationDB.Data;
using MigrationDB.Models;

namespace ExpertService.Handlers;
public class DeleteExpertsHandler : IRequestHandler<DeleteExpertsCommand, Experts>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteExpertsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<Experts> Handle(DeleteExpertsCommand request, CancellationToken cancellationToken)
    {
        var expertsList = await _dbContext.Experts.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (expertsList == null) return null;
        _dbContext.Experts.Remove(expertsList);
        await _dbContext.SaveChangesAsync();
        return expertsList;
    }
}
