using MediatR;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Queries;

public record GetSubscriberByLinkQuery : IRequest<IEnumerable<SubscriberReadDto>>
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

