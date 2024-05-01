using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdvertisingAgencyService.Queries;
public record GetAdAgencyDetailsByIdQuery(Guid Id) : IRequest<AdvertisingAgency>;


 
