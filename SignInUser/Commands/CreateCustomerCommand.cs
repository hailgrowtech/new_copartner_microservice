using MediatR;
using SignInService.Models;

namespace SignInService.Commands;

public record CreateCustomerCommand(PotentialCustomer Lead) : IRequest<PotentialCustomer>;

