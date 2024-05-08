
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands
{
    public record PutExpertsAdAgencyCommand(ExpertsAdvertisingAgency ExpertsAdvertisingAgency) : IRequest<ExpertsAdvertisingAgency>;
}
