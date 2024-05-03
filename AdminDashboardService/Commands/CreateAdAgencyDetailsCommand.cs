using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AdminDashboardService.Commands;

public record CreateAdAgencyDetailsCommand(AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;

