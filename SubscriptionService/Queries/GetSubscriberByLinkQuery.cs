using MediatR;
using MigrationDB.Model;

namespace SubscriptionService.Queries;

public record GetSubscriberByLinkQuery : IRequest<IEnumerable<Subscriber>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string Link { get; init; }

    public GetSubscriberByLinkQuery(int page, int pageSize, string link)
    {
        Page = page;
        PageSize = pageSize;
        Link = link;
    }
}

