using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdvertisingAgencyService.Commands
{
    public record DeleteAdAgencyDetailsCommand (Guid Id) : IRequest<AdvertisingAgency>;

}
