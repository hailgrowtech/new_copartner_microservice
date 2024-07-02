using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;

public record DeleteWebinarMstCommand(Guid Id) : IRequest<WebinarMst>;