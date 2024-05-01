using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using AdvertisingAgencyService.Commands;


namespace AdvertisingAgencyService.Handlers;

public class CreateAdAgencyDetailsHandler : IRequestHandler<CreateAdAgencyDetailsCommand, AdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateAdAgencyDetailsHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdvertisingAgency> Handle(CreateAdAgencyDetailsCommand request, CancellationToken cancellationToken)
    {
        var entity = request.AdvertisingAgency;
        await _dbContext.AdvertisingAgencies.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.AdvertisingAgency.Id = entity.Id;
        return request.AdvertisingAgency;
    }
}