using AffiliatePartnerService.Dtos;
using MediatR;


namespace AffiliatePartnerService.Queries;
public record GetAPListingDetailsQuery : IRequest<IEnumerable<APListingDetailDto>>;
