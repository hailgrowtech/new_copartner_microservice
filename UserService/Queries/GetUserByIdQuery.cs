using MediatR;
using MigrationDB.Models;


namespace UserService.Queries;
public record GetUserByIdQuery(Guid Id) : IRequest<User>;


 
