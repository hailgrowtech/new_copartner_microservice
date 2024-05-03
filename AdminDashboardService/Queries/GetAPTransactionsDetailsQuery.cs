using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;


public record GetAPTransactionsDetailsQuery : IRequest<IEnumerable<APTransactionsDetailsReadDto>>;
