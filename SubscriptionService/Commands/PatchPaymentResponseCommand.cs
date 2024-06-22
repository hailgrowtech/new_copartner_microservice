using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Commands;
public record PatchPaymentResponseCommand(Guid Id, JsonPatchDocument<PaymentResponseCreateDto> JsonPatchDocument, PaymentResponse Payment) : IRequest<PaymentResponse>;
