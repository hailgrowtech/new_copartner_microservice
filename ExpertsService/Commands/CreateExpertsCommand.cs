using MediatR;
using MigrationDB.Models;

namespace ExpertService.Commands;

public record CreateExpertsCommand(Experts Experts) : IRequest<Experts>;

