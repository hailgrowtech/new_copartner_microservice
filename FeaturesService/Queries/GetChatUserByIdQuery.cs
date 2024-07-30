using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetChatUserByIdQuery(Guid Id) : IRequest<ChatUser>;
