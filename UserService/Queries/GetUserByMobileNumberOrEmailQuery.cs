using MediatR;
using MigrationDB.Models;

namespace UserService.Queries;
public record GetUserByMobileNumberOrEmailQuery(User User) : IRequest<User>;



