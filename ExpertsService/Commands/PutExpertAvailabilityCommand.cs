using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;

public record PutExpertAvailabilityCommand(ExpertAvailability ExpertAvailability) : IRequest<ExpertAvailability>;
