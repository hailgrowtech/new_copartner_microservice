using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using MigrationDB.Model;
using AdminDashboardService.Dtos;


namespace AdminDashboardService.Commands;

public record PatchMarketingServiceCommand(Guid Id, JsonPatchDocument<MarketingContentCreateDto> JsonPatchDocument, MarketingContent MarketingContent) : IRequest<MarketingContent>;
