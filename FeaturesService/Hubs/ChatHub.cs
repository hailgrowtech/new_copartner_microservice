using Microsoft.AspNetCore.SignalR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Hubs;
public class ChatHub : Hub
{
    private readonly CoPartnerDbContext _context;

    public ChatHub(CoPartnerDbContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string sender, string receiver, string message)
    {
        var senderUser = _context.ChatUsers.SingleOrDefault(u => u.Username == sender);
        var receiverUser = _context.ChatUsers.SingleOrDefault(u => u.Username == receiver);

        if (senderUser == null || receiverUser == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var newMessage = new ChatMessage
        {
            Contents = message,
            Timestamp = DateTime.Now,
            SenderId = senderUser.Id,
            ReceiverId = receiverUser.Id
        };

        _context.ChatMessages.Add(newMessage);
        await _context.SaveChangesAsync();

        await Clients.User(receiverUser.ConnectionId).SendAsync("ReceiveMessage", sender, message);
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext().Request.Query["username"];
        var user = _context.ChatUsers.SingleOrDefault(u => u.Username == username);

        if (user != null)
        {
            user.ConnectionId = Context.ConnectionId;
            _context.ChatUsers.Update(user);
            await _context.SaveChangesAsync();

            // Send previous messages to the connected user
            var messages = _context.ChatMessages
                .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    Sender = m.Sender.Username,
                    Receiver = m.Receiver.Username,
                    Content = m.Contents,
                    Timestamp = m.Timestamp
                })
                .ToList();

            await Clients.Caller.SendAsync("LoadPreviousMessages", messages);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var user = _context.ChatUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

        if (user != null)
        {
            user.ConnectionId = null;
            _context.ChatUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        await base.OnDisconnectedAsync(exception);
    }
}
