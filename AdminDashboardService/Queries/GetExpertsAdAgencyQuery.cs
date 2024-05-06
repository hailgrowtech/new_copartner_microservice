using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetExpertsAdAgencyQuery : IRequest<IEnumerable<ExpertsAdvertisingAgency>>;


 
