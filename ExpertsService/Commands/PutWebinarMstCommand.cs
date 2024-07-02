using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;

public record PutWebinarMstCommand(WebinarMst webinarMst) : IRequest<WebinarMst>;