using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PutAPTransactionsDetailsCommand(APTransactionsDetailsDto APTransactionsDetailsDto) : IRequest<APTransactionsDetailsDto>;
