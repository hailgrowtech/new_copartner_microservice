using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries
{
    public class GetChatPlanQuery :  IRequest<IEnumerable<ChatPlan>>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }

        public GetChatPlanQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
