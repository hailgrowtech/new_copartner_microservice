using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdvertisingAgencyService.Queries;
public record GetAdAgencyDetailsQuery : IRequest<IEnumerable<AdvertisingAgency>>;


 
