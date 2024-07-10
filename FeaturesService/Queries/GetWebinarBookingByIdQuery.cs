using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetWebinarBookingByIdQuery(Guid Id) : IRequest<WebinarBooking>;
