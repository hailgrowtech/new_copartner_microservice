using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdvertisingAgencyService.Queries;

namespace AdvertisingAgencyService.Handlers;

public class GetAdAgencyDetailsByIdHandler : IRequestHandler<GetAdAgencyDetailsByIdQuery, AdvertisingAgency>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetAdAgencyDetailsByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdvertisingAgency> Handle(GetAdAgencyDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var adAgencyList = await _dbContext.AdvertisingAgencies.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return adAgencyList;
    }
}