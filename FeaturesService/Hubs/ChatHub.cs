using Microsoft.AspNetCore.SignalR;
using MigrationDB.Data;
using MigrationDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FeaturesService.Hubs;
public class ChatHub : Hub
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public ChatHub(CoPartnerDbContext dbContext, IConfiguration configuration)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async Task SendMessage(string sender, string receiver, string message)
    {
        var senderUser = _dbContext.ChatUsers.SingleOrDefault(u => u.Username == sender);
        var receiverUser = _dbContext.ChatUsers.SingleOrDefault(u => u.Username == receiver);

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

        _dbContext.ChatMessages.Add(newMessage);
        await _dbContext.SaveChangesAsync();

        await Clients.Client(receiverUser.ConnectionId).SendAsync("ReceiveMessage", sender, message);
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var username = Context.GetHttpContext()?.Request.Query["username"].ToString();
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException("Username query parameter is missing.");
            }

            //var user = await _dbContext.ChatUsers
            //             .AsNoTracking()
            //             .SingleOrDefaultAsync(u => u.Username == username);

            // Define your connection string
            var connectionString = _configuration.GetConnectionString("CoPartnerConnectionString");

            ChatUser user = null;
            var userQuery = "SELECT Id, Username, ConnectionId FROM ChatUser WHERE Username = @username";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var adapter = new SqlDataAdapter(userQuery, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@username", username);

                    var dataSet = new DataSet();
                    adapter.Fill(dataSet, "ChatUser");

                    if (dataSet.Tables["ChatUser"].Rows.Count > 0)
                    {
                        var row = dataSet.Tables["ChatUser"].Rows[0];
                        user = new ChatUser
                        {
                            Id = (Guid)row["Id"],
                            Username = row["Username"].ToString(),
                            ConnectionId = row["ConnectionId"].ToString()
                        };
                    }
                }
            }

            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                _dbContext.ChatUsers.Update(user);
                await _dbContext.SaveChangesAsync();

                var messages = _dbContext.ChatMessages
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
        catch (Exception ex)
        {
            // Log exception
            Console.WriteLine(ex);
            throw; // Optional: rethrow exception to preserve original behavior
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var user = _dbContext.ChatUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

        if (user != null)
        {
            user.ConnectionId = null;
            _dbContext.ChatUsers.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        await base.OnDisconnectedAsync(exception);
    }
}
