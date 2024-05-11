using ExpertsService.Dtos;
using MediatR;
using MigrationDB.Models;

namespace ExpertService.Queries;
public record GetRADetailsByIdQuery(Guid Id, int page = 1, int pageSize = 10) : IRequest<IEnumerable<RAListingDataDto>>;



