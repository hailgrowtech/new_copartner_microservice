using ExpertService.Dtos;
using ExpertsService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record PatchExpertAvailabilityCommand(Guid Id, JsonPatchDocument<ExpertAvailabilityCreateDto> JsonPatchDocument, ExpertAvailability ExpertAvailability) : IRequest<ExpertAvailability>;