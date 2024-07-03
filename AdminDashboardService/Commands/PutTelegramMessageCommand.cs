using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;


public record PutTelegramMessageCommand(TelegramMessage TelegramMessage) : IRequest<TelegramMessage>;