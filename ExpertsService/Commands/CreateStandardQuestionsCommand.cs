using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands
{

    public record CreateStandardQuestionsCommand(StandardQuestions StandardQuestions) : IRequest<StandardQuestions>;

}
