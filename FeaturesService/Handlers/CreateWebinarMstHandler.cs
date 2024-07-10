using FeaturesService.Commands;
using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
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
            //Check availability before craeteing webibnar

            var expertAvailablity = _dbContext.ExpertAvailabilities.Where(x=> x.StartTime >= request.WebinarMst.StartTime  && x.EndTime <= request.WebinarMst.EndTime).FirstOrDefault();

            if (expertAvailablity != null)
            {
                return null;
            }
            else
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
}
