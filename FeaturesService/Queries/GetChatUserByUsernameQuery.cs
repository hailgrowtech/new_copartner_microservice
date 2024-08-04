using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetChatUserByUsernameQuery(Guid Id) : IRequest<ChatUser>;
