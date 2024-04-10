using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Commands
{

    public record PatchSubscriberCommand(Guid Id, JsonPatchDocument<SubscriberCreateDto> JsonPatchDocument, Subscriber Subscriber) : IRequest<Subscriber>;
}
