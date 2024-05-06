using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record DeleteExpertsAdAgencyCommand(Guid Id) : IRequest<ExpertsAdvertisingAgency>;

}
