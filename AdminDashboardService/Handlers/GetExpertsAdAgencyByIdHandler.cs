using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using MigrationDB.Data;
using MigrationDB.Model;
using AdminDashboardService.Queries;
using AdminDashboardService.Dtos;
using MassTransit.Middleware;

namespace AdminDashboardService.Handlers;

public class GetExpertsAdAgencyByIdHandler : IRequestHandler<GetExpertsAdAgencyByIdQuery, IEnumerable<ExpertsAdAgencyDto>>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetExpertsAdAgencyByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<ExpertsAdAgencyDto>> Handle(GetExpertsAdAgencyByIdQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _dbContext.ExpertsAdvertisingAgencies
            .Where(agency => agency.IsDeleted == false && agency.AdvertisingAgencyId == request.Id)
            .GroupJoin(_dbContext.Experts,
                agency => agency.ExpertsId,
                expert => expert.Id,
                (agency, experts) => new { agency, experts })
            .SelectMany(
                ae => ae.experts.DefaultIfEmpty(),
                (ae, expert) => new { ae.agency, expert })
            .GroupJoin(_dbContext.Users,
                ae => ae.agency.Id,
                user => user.ExpertsAdvertisingAgencyId,
                (ae, users) => new { ae.agency, ae.expert, users })
            .SelectMany(
                aeu => aeu.users.DefaultIfEmpty(),
                (aeu, user) => new { aeu.agency, aeu.expert, user })
            .GroupJoin(_dbContext.Subscribers,
                aeu => aeu.user.Id,
                subscriber => subscriber.UserId,
                (aeu, subscribers) => new { aeu.agency, aeu.expert, subscribers })
            .SelectMany(
                aeus => aeus.subscribers.DefaultIfEmpty(),
                (aeus, subscriber) => new { aeus.agency, aeus.expert, subscriber })
            .GroupBy(
                result => new { ExpertName = result.expert.Name, result.agency.Link, result.agency.AdvertisingAgencyId, result.agency.Id, ExpertId = result.expert.Id })
            .Select(group => new ExpertsAdAgencyDto
            {
                ExpertsName = group.Key.ExpertName,
                AdvertisingAgencyId = group.Key.AdvertisingAgencyId,
                ExpertsAdAgencyId = group.Key.Id,
                ExpertsId = group.Key.ExpertId,
                Link = group.Key.Link,
                UsersCount = group.Select(g => g.subscriber.Id).Distinct().Count()
            })
            .ToListAsync(); // Use ToListAsync to retrieve a list of results

        return queryResult;
    }



}