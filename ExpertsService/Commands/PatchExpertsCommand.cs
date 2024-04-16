using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using MigrationDB.Models;


namespace ExpertService.Commands;

public record PatchExpertsCommand(Guid Id, JsonPatchDocument<ExpertsCreateDto> JsonPatchDocument, Experts Experts) : IRequest<Experts>;
