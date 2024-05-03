using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record DeleteAPTransactionsDetailsCommand(Guid Id) : IRequest<APTransactionsDetailsDto>;