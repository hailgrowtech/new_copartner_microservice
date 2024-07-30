using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;

public record GetChatMessageByIdQuery(Guid Id) : IRequest<IEnumerable<ChatUser>>;
