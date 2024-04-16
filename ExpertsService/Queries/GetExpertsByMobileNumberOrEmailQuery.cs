using MediatR;
using MigrationDB.Models;

namespace ExpertService.Queries;
public record GetExpertsByMobileNumberOrEmailQuery(Experts Experts) : IRequest<Experts>;



