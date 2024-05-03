using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;


namespace AdminDashboardService.Commands;

public record PatchBlogCommand(Guid Id, JsonPatchDocument<BLogCreateDto> JsonPatchDocument, Blog Blog) : IRequest<Blog>;
