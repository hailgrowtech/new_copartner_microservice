using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries;


public record GetWebinarBookingByIdQuery(Guid Id) : IRequest<WebinarBooking>;
