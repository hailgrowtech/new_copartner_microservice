using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;

public record GetWebinarMstByIdQuery(Guid Id) : IRequest<WebinarMst>;
