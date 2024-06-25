using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;

using UserService.Queries;
using static MassTransit.ValidationResultExtensions;

namespace UserService.Handlers;
public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetUsersHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;

        var entities =  await _dbContext.Users.Where(x => x.IsDeleted != true)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        if (entities == null) return null;
        return entities.Select(e => e.ConvertAllDateTimesToIST()).ToList();
       
    }
}