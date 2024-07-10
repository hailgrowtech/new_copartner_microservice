using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;


public record PutWebinarBookingCommand(WebinarBooking WebinarBooking) : IRequest<WebinarBooking>;