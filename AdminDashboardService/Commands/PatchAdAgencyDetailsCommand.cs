using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;
using AdminDashboardService.Dtos;


namespace AdminDashboardService.Commands;

public record PatchAdAgencyDetailsCommand(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> JsonPatchDocument, AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;
