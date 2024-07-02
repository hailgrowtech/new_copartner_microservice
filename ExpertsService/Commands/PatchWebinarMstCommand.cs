using ExpertService.Dtos;
using ExpertsService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace ExpertsService.Commands;

public record PatchWebinarMstCommand(Guid Id, JsonPatchDocument<WebinarMstCreateDto> JsonPatchDocument, WebinarMst webinarMst) : IRequest<WebinarMst>;
