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

            // Query to replicate the SQL logic
            var result = await (from message in _dbContext.ChatMessages
                                join user in _dbContext.ChatUsers
                                on message.SenderId equals user.Id into senderUsers
                                from senderUser in senderUsers.DefaultIfEmpty()
                                join user in _dbContext.ChatUsers
                                on message.ReceiverId equals user.Id into receiverUsers
                                from receiverUser in receiverUsers.DefaultIfEmpty()
                                where message.SenderId == request.Id || message.ReceiverId == request.Id
                                select senderUser ?? receiverUser)
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




