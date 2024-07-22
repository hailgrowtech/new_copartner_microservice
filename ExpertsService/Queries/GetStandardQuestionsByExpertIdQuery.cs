using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries
{

    public record GetStandardQuestionsByExpertIdQuery : IRequest<IEnumerable<StandardQuestions>>
    {
        public Guid ExpertId { get; set; }
        public int Page { get; init; }
        public int PageSize { get; init; }

        public GetStandardQuestionsByExpertIdQuery(Guid ExpertyId, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            ExpertId = ExpertyId;
        }
    }
}
