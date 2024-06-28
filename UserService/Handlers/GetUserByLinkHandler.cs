using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Dtos;
using UserService.Queries;

namespace UserService.Handlers
{

    public class GetUserByLinkHandler : IRequestHandler<GetUserByLinkQuery, IEnumerable<User>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetUserByLinkHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> Handle(GetUserByLinkQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;

            var entities = await _dbContext.Users.Where(x => x.IsDeleted != true && x.LandingPageUrl == request.link)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
