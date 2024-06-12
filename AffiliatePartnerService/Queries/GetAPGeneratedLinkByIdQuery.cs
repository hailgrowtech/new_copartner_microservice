using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AffiliatePartnerService.Queries;

public class GetAPGeneratedLinkByIdQuery : IRequest<IEnumerable<APGeneratedLinkReadDTO>>
{
    public Guid Id { get; }

    public GetAPGeneratedLinkByIdQuery(Guid id)
    {
        Id = id;
    }
}
