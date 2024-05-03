using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetAdAgencyDetailsQuery : IRequest<IEnumerable<AdvertisingAgency>>;


 
