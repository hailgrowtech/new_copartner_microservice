using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;


public record CreateWebinarMstCommand(WebinarMst WebinarMst) : IRequest<WebinarMst>;