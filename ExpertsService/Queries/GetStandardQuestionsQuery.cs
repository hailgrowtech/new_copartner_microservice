using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries
{

    public record GetStandardQuestionsQuery : IRequest<IEnumerable<StandardQuestions>>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }

        public GetStandardQuestionsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
