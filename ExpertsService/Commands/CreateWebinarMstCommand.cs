using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record CreateWebinarMstCommand(WebinarMst WebinarMst) : IRequest<WebinarMst>;