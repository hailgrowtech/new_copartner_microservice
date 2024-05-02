
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdvertisingAgencyService.Commands
{
    public record PutAdAgencyDetailsCommand(AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;
}
