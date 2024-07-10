using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record PutWebinarMstCommand(WebinarMst webinarMst) : IRequest<WebinarMst>;