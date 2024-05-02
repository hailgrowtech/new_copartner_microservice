using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using MigrationDB.Model;
using MarketingContentService.Dtos;


namespace MarketingContentService.Commands;

public record PatchMarketingServiceCommand(Guid Id, JsonPatchDocument<MarketingContentCreateDto> JsonPatchDocument, MarketingContent MarketingContent) : IRequest<MarketingContent>;
