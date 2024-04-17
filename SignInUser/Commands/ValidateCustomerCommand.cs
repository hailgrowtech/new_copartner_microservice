using MediatR;
using SignInService.Models;

namespace SignInService.Commands;

public record ValidateCustomerCommand(PotentialCustomer Lead, bool isLeadUpdateRequest = false) : IRequest<PotentialCustomer>;

