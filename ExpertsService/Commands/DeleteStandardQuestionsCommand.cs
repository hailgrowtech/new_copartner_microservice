using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands
{

    public record DeleteStandardQuestionsCommand(Guid Id) : IRequest<StandardQuestions>;

}
