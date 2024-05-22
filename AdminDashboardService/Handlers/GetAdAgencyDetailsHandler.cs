using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Queries;
using MassTransit;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Handlers;
public class GetAdAgencyDetailsHandler : IRequestHandler<GetAdAgencyDetailsQuery, IEnumerable<AdAgencyDetailsDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetAdAgencyDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

   public async Task<IEnumerable<AdAgencyDetailsDto>> Handle(GetAdAgencyDetailsQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.Page - 1) * request.PageSize;

        var entities = await _dbContext.AdvertisingAgencies
            .Where(agency => agency.IsDeleted !=true)
            .GroupJoin(_dbContext.Users,
                agency => agency.Id,
                user => user.AdvertisingAgencyId,
                (agency, users) => new { agency, users })
            .SelectMany(
                au => au.users.DefaultIfEmpty(),
                (au, user) => new { au.agency, user })
            .GroupJoin(_dbContext.Subscribers,
                au => au.user.Id,
                subscriber => subscriber.UserId,
                (au, subscribers) => new { au.agency, subscribers })
            .SelectMany(
                aus => aus.subscribers.DefaultIfEmpty(),
                (aus, subscriber) => new { aus.agency, subscriber })
            .GroupBy(
                result => new { result.agency.Id, result.agency.AgencyName, result.agency.CreatedOn, result.agency.Link,result.agency.IsDeleted })
            .Select(group => new AdAgencyDetailsDto
            {
                Id = group.Key.Id,
                JoinDate = group.Key.CreatedOn,
                AgencyName = group.Key.AgencyName,
                Link = group.Key.Link,
                UsersCount = group.Select(g => g.subscriber.Id).Distinct().Count(),
                isActive = !group.Key.IsDeleted

            })
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

            if (entities == null) return null;
            return entities;
    }

}