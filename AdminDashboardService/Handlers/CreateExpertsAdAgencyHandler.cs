using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;


namespace AdminDashboardService.Handlers;

public class CreateExpertsAdAgencyHandler : IRequestHandler<CreateExpertsAdAgencyCommand, ExpertsAdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateExpertsAdAgencyHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExpertsAdvertisingAgency> Handle(CreateExpertsAdAgencyCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ExpertsAdvertisingAgency;
        await _dbContext.ExpertsAdvertisingAgencies.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.ExpertsAdvertisingAgency.Id = entity.Id;
        return request.ExpertsAdvertisingAgency;
    }
}