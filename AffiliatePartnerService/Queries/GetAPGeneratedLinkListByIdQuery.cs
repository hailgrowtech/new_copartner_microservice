using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AffiliatePartnerService.Queries;

public class GetAPGeneratedLinkListByIdQuery : IRequest<IEnumerable<APGeneratedLinkReadDTO>>
{
    public Guid Id { get; }

    public GetAPGeneratedLinkListByIdQuery(Guid id)
    {
        Id = id;
    }
}
