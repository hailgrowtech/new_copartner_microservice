using ExpertService.Commands;
using ExpertsService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class CreateWebinarMstHandler : IRequestHandler<CreateWebinarMstCommand, WebinarMst>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateWebinarMstHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebinarMst> Handle(CreateWebinarMstCommand request, CancellationToken cancellationToken)
        {
            var entity = request.WebinarMst;
            await _dbContext.WebinarMsts.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.WebinarMst.Id = entity.Id;
            //request.Experts.isActive = entity.isActive;
            return request.WebinarMst;
        }
    }
}
