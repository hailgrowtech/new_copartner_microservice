using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Commands
{
    public record PatchSubscriptionCommand(Guid Id, JsonPatchDocument<SubscriptionCreateDto> JsonPatchDocument, Subscription Subscription) : IRequest<Subscription>;

}
