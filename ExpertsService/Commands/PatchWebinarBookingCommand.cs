using ExpertsService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record PatchWebinarBookingCommand(Guid Id, JsonPatchDocument<WebinarBookingCreateDto> JsonPatchDocument, WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;
