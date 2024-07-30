using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetChatUserByIdHandler : IRequestHandler<GetChatUserByIdQuery, ChatUser>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetChatUserByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatUser> Handle(GetChatUserByIdQuery request, CancellationToken cancellationToken)
        {
            var chatUser = await _dbContext.ChatUsers.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return chatUser;
        }
    }
}
