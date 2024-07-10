using CommonLibrary.Extensions;
using FeaturesService.Queries;
using FeaturesService.Dtos;
using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetWebinarMstHandler : IRequestHandler<GetWebinarMstQuery, IEnumerable<WebinarMst>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetWebinarMstHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<WebinarMst>> Handle(GetWebinarMstQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;
            var entities = await _dbContext.WebinarMsts.Where(x => x.IsDeleted != true)
                         .OrderByDescending(x => x.CreatedOn)
                         .Skip(skip)
                         .Take(request.PageSize)
                         .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;
        }



    }
}
