using FeaturesService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers
{
    public class GetChatUserByUsernameHandler : IRequestHandler<GetChatUserByUsernameQuery, ChatUser>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetChatUserByUsernameHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatUser> Handle(GetChatUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var chatUser = await _dbContext.ChatUsers.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return chatUser;
        }
    }
}
