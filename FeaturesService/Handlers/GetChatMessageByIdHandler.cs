using FeaturesService.Queries;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers;
public class GetChatMessageByIdHandler : IRequestHandler<GetChatMessageByIdQuery, IEnumerable<ChatUser>>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetChatMessageByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ChatUser>> Handle(GetChatMessageByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = request.Id;

            var result = await (
                from message in _dbContext.ChatMessages
                join sender in _dbContext.ChatUsers on message.SenderId equals sender.Id into senderGroup
                from senderUser in senderGroup.DefaultIfEmpty()
                join receiver in _dbContext.ChatUsers on message.ReceiverId equals receiver.Id into receiverGroup
                from receiverUser in receiverGroup.DefaultIfEmpty()
                where message.SenderId == userId || message.ReceiverId == userId
                select new
                {
                    UserId = message.SenderId == userId ? receiverUser.Id : senderUser.Id,
                    Username = message.SenderId == userId ? receiverUser.Username : senderUser.Username,
                    UserType = message.SenderId == userId ? receiverUser.UserType : senderUser.UserType
                }
            ).Distinct().ToListAsync(cancellationToken);

            // Map the result to IEnumerable<ChatUser>
            var chatUsers = result.Select(r => new ChatUser
            {
                Id = r.UserId,
                Username = r.Username,
                UserType = r.UserType
            });

            return chatUsers;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);

            return Enumerable.Empty<ChatUser>();
        }
    }
}




