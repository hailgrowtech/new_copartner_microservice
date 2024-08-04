using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record DeleteChatPlanCommand(Guid Id) : IRequest<ChatPlan>;