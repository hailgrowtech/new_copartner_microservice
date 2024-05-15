using ExpertsService.Dtos;
using MediatR;
using System.Drawing.Printing;

namespace ExpertService.Queries;
public record GetRADetailsQuery : IRequest<IEnumerable<RADetailsDto>>
{
    public bool IsCoPartner { get; set; }
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetRADetailsQuery(bool isCoPartner,int page, int pageSize)
    {
        IsCoPartner = isCoPartner;
        Page = page;
        PageSize = pageSize;
    }
}
