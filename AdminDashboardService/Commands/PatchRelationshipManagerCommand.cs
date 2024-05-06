using AdminDashboardService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PatchRelationshipManagerCommand(Guid Id, JsonPatchDocument<RelationshipManagerCreateDto> JsonPatchDocument, RelationshipManager RelationshipManager) : IRequest<RelationshipManager>;
