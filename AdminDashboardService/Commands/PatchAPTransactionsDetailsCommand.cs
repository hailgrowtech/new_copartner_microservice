using AdminDashboardService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Commands;

public record PatchAPTransactionsDetailsCommand(Guid Id, JsonPatchDocument<APTransactionsDetailsCreateDto> JsonPatchDocument, APTransactionsDetailsDto APTransactionsDetailsDto) : IRequest<APTransactionsDetailsDto>;

