using AdminDashboardService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AdminDashboardService.Commands;

public record PatchTelegramMessageCommand(Guid Id, JsonPatchDocument<TelegramMessageCreateDto> JsonPatchDocument, TelegramMessage TelegramMessage) : IRequest<TelegramMessage>;
