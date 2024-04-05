using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Queries;

namespace UserService.Handlers;
public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly UserDbContext _dbContext;
    public GetUsersHandler(UserDbContext dbContext) => _dbContext = dbContext;
    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.Users.ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}