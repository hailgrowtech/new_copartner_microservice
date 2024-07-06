using MediatR;
using MigrationDB.Model;

namespace ExpertsService.Commands;


public record DeleteExpertAvailabilityCommand(Guid Id) : IRequest<ExpertAvailability>;
