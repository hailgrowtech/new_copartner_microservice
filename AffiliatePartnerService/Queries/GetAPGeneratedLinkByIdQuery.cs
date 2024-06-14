using MediatR;
using MigrationDB.Model;

namespace AffiliatePartnerService.Queries;

public record GetAPGeneratedLinkByIdQuery(Guid Id) : IRequest<APGeneratedLinks>;
