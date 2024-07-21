using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly CoPartnerDbContext _dbContext;

        public ChatHub(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            var senderUser = await _dbContext.ChatUsers.SingleOrDefaultAsync(u => u.Username == sender);
            var receiverUser = await _dbContext.ChatUsers.SingleOrDefaultAsync(u => u.Username == receiver);

            if (senderUser == null || receiverUser == null)
            {
                throw new InvalidOperationException("Sender or receiver not found.");
            }

            var newMessage = new ChatMessage
            {
                Contents = message,
                Timestamp = DateTime.UtcNow,
                SenderId = senderUser.Id,
                ReceiverId = receiverUser.Id
            };

            await _dbContext.ChatMessages.AddAsync(newMessage);
            await _dbContext.SaveChangesAsync();

            if (!string.IsNullOrEmpty(receiverUser.ConnectionId))
            {
                await Clients.Client(receiverUser.ConnectionId).SendAsync("ReceiveMessage", sender, message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext()?.Request.Query["username"].ToString();
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException("Username is required.");
            }

            var user = await _dbContext.ChatUsers.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.ConnectionId = Context.ConnectionId;
            _dbContext.ChatUsers.Update(user);
            await _dbContext.SaveChangesAsync();

            var messages = await _dbContext.ChatMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    Sender = m.Sender.Username,
                    Receiver = m.Receiver.Username,
                    Content = m.Contents,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            await Clients.Caller.SendAsync("LoadPreviousMessages", messages);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _dbContext.ChatUsers.SingleOrDefaultAsync(u => u.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                user.ConnectionId = null;
                _dbContext.ChatUsers.Update(user);
                await _dbContext.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
