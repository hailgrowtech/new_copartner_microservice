using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetTelegramMessageByIdAPRAQuery(Guid Id, string userType, int page, int pageSize) : IRequest<IEnumerable<TelegramMessageReadDto>>;