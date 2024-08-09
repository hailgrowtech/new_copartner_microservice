using MediatR;
using MigrationDB.Models;


namespace UserService.Queries;
public record GetUserByMobileNumberQuery(string? MobileNumber) : IRequest<User>;


 
