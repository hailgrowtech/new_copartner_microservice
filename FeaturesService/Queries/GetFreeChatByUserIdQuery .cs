using FeaturesService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetFreeChatByUserIdQuery(Guid Id) : IRequest<IEnumerable<FreeChat>>;
