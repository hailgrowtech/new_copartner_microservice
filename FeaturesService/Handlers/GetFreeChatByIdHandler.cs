using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetFreeChatByIdHandler : IRequestHandler<GetFreeChatByIdQuery, FreeChat>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetFreeChatByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FreeChat> Handle(GetFreeChatByIdQuery request, CancellationToken cancellationToken)
        {
            var freeChat = await _dbContext.FreeChats.Where(a => a.UserId == request.UserId && a.ExpertsId== request.ExpertsId).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return freeChat;
        }
    }
}
