using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;
using AdminDashboardService.Dtos;


namespace AdminDashboardService.Commands;

public record PatchExpertsAdAgencyCommand(Guid Id, JsonPatchDocument<ExpertsAdAgencyCreateDto> JsonPatchDocument, ExpertsAdvertisingAgency ExpertsAdvertisingAgency) : IRequest<ExpertsAdvertisingAgency>;
