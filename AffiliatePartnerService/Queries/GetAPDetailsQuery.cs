using AffiliatePartnerService.Dtos;
using MediatR;


namespace AffiliatePartnerService.Queries;
public record GetAPDetailsQuery : IRequest<IEnumerable<APDetailDto>>;
