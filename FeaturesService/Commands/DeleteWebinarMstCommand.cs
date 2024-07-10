using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record DeleteWebinarMstCommand(Guid Id) : IRequest<WebinarMst>;