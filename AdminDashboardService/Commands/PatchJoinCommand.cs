using AdminDashboardService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PatchJoinCommand(Guid Id, JsonPatchDocument<JoinCreateDto> JsonPatchDocument, Join Join) : IRequest<Join>;

