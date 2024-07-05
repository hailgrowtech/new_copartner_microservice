using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;



public record DeleteTelegramMessageCommand(Guid Id) : IRequest<TelegramMessage>;