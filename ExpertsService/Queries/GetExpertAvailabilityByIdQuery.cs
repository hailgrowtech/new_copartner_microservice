using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Queries;


public record GetExpertAvailabilityByIdQuery(Guid Id) : IRequest<ExpertAvailability>;

