using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Commands;
using Microsoft.EntityFrameworkCore;


namespace AdminDashboardService.Handlers;

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

        // Check if the combination of AgencyName and Link is unique
        bool isUnique = await IsAgencyNameLinkUnique(entity.AgencyName, entity.Link);

        if (!isUnique)
        {
            return null;
        }

        await _dbContext.AdvertisingAgencies.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.AdvertisingAgency.Id = entity.Id;
        return request.AdvertisingAgency;
    }
    private async Task<bool> IsAgencyNameLinkUnique(string agencyName, string link)
    {
        // Normalize input to lowercase
        string lowerCaseAgencyName = agencyName.ToLower();
        string lowerCaseLink = link.ToLower();

        // Check if any existing entity has the same AgencyName and Link combination (case-insensitive)
        var existingEntity = await _dbContext.AdvertisingAgencies
            .FirstOrDefaultAsync(a => a.AgencyName.ToLower() == lowerCaseAgencyName && a.Link.ToLower() == lowerCaseLink);

        // Return true if no existing entity is found, indicating uniqueness
        return existingEntity == null;
    }
}