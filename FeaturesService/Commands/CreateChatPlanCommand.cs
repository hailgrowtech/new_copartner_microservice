using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record CreateChatPlanCommand(ChatPlan ChatPlan) : IRequest<ChatPlan>;