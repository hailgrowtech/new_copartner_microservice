using MediatR;
using UserService.Models;

namespace UserService.Queries;
public record GetUserByMobileNumberOrEmailQuery(User User) : IRequest<User>;



