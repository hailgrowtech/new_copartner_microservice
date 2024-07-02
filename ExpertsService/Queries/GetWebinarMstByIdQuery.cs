using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries;

public record GetWebinarMstByIdQuery(Guid Id) : IRequest<WebinarMst>;
