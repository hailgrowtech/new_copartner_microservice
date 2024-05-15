using ExpertsService.Dtos;
using MediatR;
using System.Drawing.Printing;


namespace ExpertService.Queries;
public record GetRAListingQuery : IRequest<IEnumerable<RAListingDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetRAListingQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
