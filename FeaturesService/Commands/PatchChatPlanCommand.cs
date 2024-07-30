using FeaturesService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace FeaturesService.Commands;


public record PatchChatPlanCommand(Guid Id, JsonPatchDocument<ChatPlanCreateDto> JsonPatchDocument, ChatPlan ChatPlan) : IRequest<ChatPlan>;
