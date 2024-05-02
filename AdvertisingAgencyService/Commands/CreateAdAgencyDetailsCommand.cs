using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdvertisingAgencyService.Commands;

public record CreateAdAgencyDetailsCommand(AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;

