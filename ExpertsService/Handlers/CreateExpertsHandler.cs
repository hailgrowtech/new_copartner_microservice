using MediatR;
using ExpertService.Commands;
using ExpertService.Data;
using ExpertService.Models;

namespace ExpertService.Handlers;
public class CreateExpertsHandler : IRequestHandler<CreateExpertsCommand, Experts>
{
    private readonly ExpertsDbContext _dbContext;
    public CreateExpertsHandler(ExpertsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Experts> Handle(CreateExpertsCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Experts;
        await _dbContext.Experts.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Experts.Id = entity.Id;
        return request.Experts;
    }
}