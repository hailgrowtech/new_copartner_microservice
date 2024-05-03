using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record DeleteAdAgencyDetailsCommand (Guid Id) : IRequest<AdvertisingAgency>;

}
