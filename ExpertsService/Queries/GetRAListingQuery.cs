using ExpertsService.Dtos;
using MediatR;


namespace ExpertService.Queries;
public record GetRAListingQuery : IRequest<IEnumerable<RAListingDto>>;
