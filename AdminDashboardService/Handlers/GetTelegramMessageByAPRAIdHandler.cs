using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetTelegramMessageByAPRAIdHandler : IRequestHandler<GetTelegramMessageByIdAPRAQuery, IEnumerable<TelegramMessageReadDto>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetTelegramMessageByAPRAIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TelegramMessageReadDto>> Handle(GetTelegramMessageByIdAPRAQuery request, CancellationToken cancellationToken)
        {
            //var requestBy = await (from tm in _dbContext.TelegramMessages
            //                       join e in _dbContext.Experts
            //                       on tm.ExpertId equals e.Id
            //                       where tm.Id == request.Id
            //                       select tm.AssignedTo != null ? "AP" : "RA")
            //                     .FirstOrDefaultAsync();
            if (request.userType == null)
                return null;


            IQueryable<TelegramMessageReadDto> query = Enumerable.Empty<TelegramMessageReadDto>().AsQueryable();
            if (request.userType.ToLower() == "RA".ToLower())
            {
                query = _dbContext.TelegramMessages
               .Where(w => w.AssignedTo == "RA" && w.ExpertsId == request.Id && w.IsDeleted != true) 
               .Join(
                   _dbContext.Experts,
                   combined => combined.ExpertsId,
                   expert => expert.Id,
                   (combined, expert) => new TelegramMessageReadDto
                   {
                       Id = combined.Id,
                       ChannelName = combined.ChannelName,
                       ChatId = combined.ChatId,
                       JoinMessage = combined.JoinMessage,
                       LeaveMessage = combined.LeaveMessage,
                       MarketingMessage = combined.MarketingMessage,
                       AssignedTo = combined.AssignedTo,
                       ExpertsId = combined.ExpertsId,
                       ExpertsName = combined.ExpertsName,
                       AffiliatePartnersId = combined.AffiliatePartnersId,
                       AffiliatePartnersName = combined.AffiliatePartnersName,
                   });
            }

            else if (request.userType.ToLower() == "AP".ToLower())
            {
                query = _dbContext.TelegramMessages
               .Where(w => w.AssignedTo == "AP" && w.AffiliatePartnersId == request.Id && w.IsDeleted != true)
               .Join(
                   _dbContext.Experts,
                   combined => combined.ExpertsId,
                   expert => expert.Id,
                   (combined, expert) => new TelegramMessageReadDto
                   {
                       Id = combined.Id,
                       ChannelName = combined.ChannelName,
                       ChatId = combined.ChatId,
                       JoinMessage = combined.JoinMessage,
                       LeaveMessage = combined.LeaveMessage,
                       MarketingMessage = combined.MarketingMessage,
                       AssignedTo = combined.AssignedTo,
                       ExpertsId = combined.ExpertsId,
                       ExpertsName = combined.ExpertsName,
                       AffiliatePartnersId = combined.AffiliatePartnersId,
                       AffiliatePartnersName = combined.AffiliatePartnersName,
                   });
            }

            var result = await query.ToListAsync(cancellationToken: cancellationToken);

            if (result == null) return null;
            return result;
        }
    }
}
