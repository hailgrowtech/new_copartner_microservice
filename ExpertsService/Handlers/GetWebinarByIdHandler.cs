using ExpertService.Queries;
using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetWebinarByIdHandler : IRequestHandler<GetWebinarMstByIdQuery, WebinarMst>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetWebinarByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebinarMst> Handle(GetWebinarMstByIdQuery request, CancellationToken cancellationToken)
        {
            var webinar = await _dbContext.WebinarMsts.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return webinar;
        }

    }
}
