using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries
{

    public record GetStandardQuestionByIdQuery(Guid Id) : IRequest<StandardQuestions>;

}
