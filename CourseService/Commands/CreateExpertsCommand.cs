using MediatR;
using ExpertService.Models;

namespace ExpertService.Commands;

public record CreateExpertsCommand(Experts Experts) : IRequest<Experts>;

