using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands;

public record CreateExpertsAdAgencyCommand(ExpertsAdvertisingAgency ExpertsAdvertisingAgency) : IRequest<ExpertsAdvertisingAgency>;

