using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AffiliatePartnerService.Queries;
public record GetAPListingQuery : IRequest<IEnumerable<APListingDto>>;

