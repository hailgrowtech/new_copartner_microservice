using ExpertService.Dtos;
using ExpertsService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace ExpertsService.Commands
{

    public record PatchStandardQuestionsCommand(Guid Id, JsonPatchDocument<StandardQuestionsCreateDto> JsonPatchDocument, StandardQuestions StandardQuestions) : IRequest<StandardQuestions>;

}
