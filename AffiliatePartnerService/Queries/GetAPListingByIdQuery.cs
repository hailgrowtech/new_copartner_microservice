using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Models;

namespace ExpertService.Queries;
public record GetAPListingByIdQuery(Guid Id, int page = 1, int pageSize = 10) : IRequest<IEnumerable<APListingDataDto>>;



