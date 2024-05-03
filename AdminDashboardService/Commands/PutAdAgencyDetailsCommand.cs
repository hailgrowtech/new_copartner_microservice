
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record PutAdAgencyDetailsCommand(AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;
}
