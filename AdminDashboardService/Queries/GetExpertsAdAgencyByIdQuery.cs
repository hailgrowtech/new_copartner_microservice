using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetExpertsAdAgencyByIdQuery(Guid Id) : IRequest<IEnumerable<ExpertsAdAgencyDto>>;


 
