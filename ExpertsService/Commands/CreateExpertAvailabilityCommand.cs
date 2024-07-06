using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record CreateExpertAvailabilityCommand(ExpertAvailability ExpertAvailability) : IRequest<ExpertAvailability>;