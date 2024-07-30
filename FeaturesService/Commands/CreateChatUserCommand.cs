using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record CreateChatUserCommand(ChatUser ChatUser) : IRequest<ChatUser>;