using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;
public record GetFreeChatByIdQuery(Guid UserId, Guid ExpertsId) : IRequest<FreeChat>;
