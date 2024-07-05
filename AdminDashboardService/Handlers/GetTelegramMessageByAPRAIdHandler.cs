using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetTelegramMessageByAPRAIdHandler : IRequestHandler<GetTelegramMessageByIdAPRAQuery, IEnumerable<TelegramMessage>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetTelegramMessageByAPRAIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TelegramMessage>> Handle(GetTelegramMessageByIdAPRAQuery request, CancellationToken cancellationToken)
        {


            if (request.userType == null)
                return null;

            if (request.userType.Equals("RA", StringComparison.OrdinalIgnoreCase))
            {
                var result = await _dbContext.TelegramMessages
                    .Where(w => w.AssignedTo == "RA" && w.ExpertsId == request.Id && w.IsDeleted != true)
                    .ToListAsync(cancellationToken: cancellationToken);

                return result;
            }
            else if (request.userType.Equals("AP", StringComparison.OrdinalIgnoreCase))
            {
                var result = await _dbContext.TelegramMessages
                    .Where(w => w.AssignedTo == "RA" && w.ExpertsId == request.Id && w.IsDeleted != true)
                    .ToListAsync(cancellationToken: cancellationToken);

                return result;
            }

            return null;
        }
    
    }
}
