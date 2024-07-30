using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetChatPlanByIdQuery(Guid Id) : IRequest<ChatPlan>;
