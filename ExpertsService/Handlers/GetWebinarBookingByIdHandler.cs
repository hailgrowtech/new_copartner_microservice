using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetWebinarBookingByIdHandler : IRequestHandler<GetWebinarBookingByIdQuery, WebinarBooking>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetWebinarBookingByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebinarBooking> Handle(GetWebinarBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var webinar = await _dbContext.WebinarBookings.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return webinar;
        }
    }
}
