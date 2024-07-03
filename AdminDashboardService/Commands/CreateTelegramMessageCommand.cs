using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;

public record CreateTelegramMessageCommand(TelegramMessage telegramMessage) : IRequest<TelegramMessage>;