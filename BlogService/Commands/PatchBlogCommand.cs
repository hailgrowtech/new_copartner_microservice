using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using MigrationDB.Models;
using BlogService.Dtos;
using MigrationDB.Model;


namespace BlogService.Commands;

public record PatchBlogCommand(Guid Id, JsonPatchDocument<BLogCreateDto> JsonPatchDocument, Blog Blog) : IRequest<Blog>;
