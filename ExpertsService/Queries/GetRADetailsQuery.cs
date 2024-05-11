using ExpertsService.Dtos;
using MediatR;
using System.Drawing.Printing;

namespace ExpertService.Queries;
public record GetRADetailsQuery : IRequest<IEnumerable<RADetailsDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetRADetailsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
