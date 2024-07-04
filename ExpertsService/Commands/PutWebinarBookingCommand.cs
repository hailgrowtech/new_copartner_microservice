using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record PutWebinarBookingCommand(WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;