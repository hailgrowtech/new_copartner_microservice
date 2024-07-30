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

            // Fetch users who are either senders or receivers of chat messages, excluding the user with the given Id
            var result = await (from user in _dbContext.ChatUsers
                                join senderMessage in _dbContext.ChatMessages
                                on user.Id equals senderMessage.SenderId into senderMessages
                                from senderMessage in senderMessages.DefaultIfEmpty()
                                join receiverMessage in _dbContext.ChatMessages
                                on user.Id equals receiverMessage.ReceiverId into receiverMessages
                                from receiverMessage in receiverMessages.DefaultIfEmpty()
                                where (senderMessage != null || receiverMessage != null)
                                      && (senderMessage == null || !senderMessage.IsDeleted)
                                      && (receiverMessage == null || !receiverMessage.IsDeleted)
                                      && user.Id != userId
                                select new ChatUser
                                {
                                    Id = user.Id,
                                    Username = user.Username,
                                    UserType = user.UserType
                                })
                    .Distinct()
                    .ToListAsync(cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            // Log the exception details (you can use a logging library or framework here)
            // For demonstration, we use Console.WriteLine, but in production, use a logging framework
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);

            // Return an empty list instead of null
            return Enumerable.Empty<ChatUser>();
        }
    }
}




