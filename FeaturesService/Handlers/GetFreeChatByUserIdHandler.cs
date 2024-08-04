using CommonLibrary.Extensions;
using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetFreeChatByUserIdHandler : IRequestHandler<GetFreeChatByUserIdQuery, IEnumerable<FreeChat>>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetFreeChatByUserIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FreeChat>> Handle(GetFreeChatByUserIdQuery request, CancellationToken cancellationToken)
        {
            var freeChat = await _dbContext.FreeChats.Where(a => a.UserId == request.Id && a.IsDeleted != true).ToListAsync(cancellationToken);
            return freeChat.Select(e => e.ConvertAllDateTimesToIST()).ToList();
        }
    }
}
