using ExpertsService.Dtos;
using MediatR;

namespace ExpertService.Queries;
public record GetRAListingDetailsQuery : IRequest<IEnumerable<RAListingDetailsDto>>;
