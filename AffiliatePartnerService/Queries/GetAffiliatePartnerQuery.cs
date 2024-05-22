using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AffiliatePartnerService.Queries;
public record GetAffiliatePartnerQuery : IRequest<IEnumerable<AffiliatePartner>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetAffiliatePartnerQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


 
