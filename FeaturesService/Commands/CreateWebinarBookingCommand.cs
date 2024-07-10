using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record CreateWebinarBookingCommand(WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;