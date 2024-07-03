using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;


public record GetTelegramMessageByIdQuery(Guid Id) : IRequest<TelegramMessage>;