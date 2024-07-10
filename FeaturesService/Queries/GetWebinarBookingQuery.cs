using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries
{
    public class GetWebinarBookingQuery :  IRequest<IEnumerable<WebinarBooking>>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }

        public GetWebinarBookingQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
