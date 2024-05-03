using AdminDashboardService.Dtos;
using MediatR;

namespace AdminDashboardService.Queries;

public record GetAPTransactionsDetailsByIdQuery(Guid Id) : IRequest<APTransactionsDetailsDto>;
