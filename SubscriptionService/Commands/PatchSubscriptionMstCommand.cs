using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Commands
{
    public record PatchSubscriptionMstCommand(Guid Id, JsonPatchDocument<SubscriptionCreateDto> JsonPatchDocument, SubscriptionMst SubscriptionMst) : IRequest<SubscriptionMst>;

}
