using MediatR;
using SignInService.Models;

namespace SignInService.Queries;
public record GetExistingOtpAttemptsQuery(PotentialCustomer Lead) : IRequest<PotentialCustomer>;


 
