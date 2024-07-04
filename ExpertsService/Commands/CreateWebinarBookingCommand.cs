using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;

public record CreateWebinarBookingCommand(WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;