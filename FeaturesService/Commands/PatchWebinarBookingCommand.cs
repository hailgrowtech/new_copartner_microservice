using FeaturesService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace FeaturesService.Commands;


public record PatchWebinarBookingCommand(Guid Id, JsonPatchDocument<WebinarBookingCreateDto> JsonPatchDocument, WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;
