using FeaturesService.Dtos;
using FeaturesService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record PatchWebinarMstCommand(Guid Id, JsonPatchDocument<WebinarMstCreateDto> JsonPatchDocument, WebinarMst webinarMst) : IRequest<WebinarMst>;
