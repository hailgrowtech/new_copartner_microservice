using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class CreateWebinarBookingHandler : IRequestHandler<CreateWebinarBookingCommand, WebinarBooking>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateWebinarBookingHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebinarBooking> Handle(CreateWebinarBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = request.WebinarBooking;
            await _dbContext.WebinarBookings.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.WebinarBooking.Id = entity.Id;
            //request.Experts.isActive = entity.isActive;
            return request.WebinarBooking;
        }
    }
}
