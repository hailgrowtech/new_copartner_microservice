using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesService.Hubs;
public class ChatHub : Hub
{
    private readonly CoPartnerDbContext _dbContext;
    private readonly IConfiguration _configuration; // IConfiguration to access appsettings.json
    private readonly string _connectionString; // Connection string for your database
    public ChatHub(CoPartnerDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("CoPartnerConnectionString");
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
            Timestamp = DateTime.Now,
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
        var chatPartnerUsername = Context.GetHttpContext()?.Request.Query["chatPartnerUsername"].ToString();
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(chatPartnerUsername))
        {
            throw new InvalidOperationException("Username is required.");
        }

        // Fetch the user from the database
        var user = await GetUserByUsernameAsync(username);
        var chatPartner = await GetUserByUsernameAsync(chatPartnerUsername);
        // var user = await _dbContext.ChatUsers.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null || chatPartner == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        user.ConnectionId = Context.ConnectionId;
        _dbContext.ChatUsers.Update(user);
        await _dbContext.SaveChangesAsync();

        var messages = await _dbContext.ChatMessages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
           // .Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id)
             .Where(m => (m.SenderId == user.Id && m.ReceiverId == chatPartner.Id) ||
                        (m.SenderId == chatPartner.Id && m.ReceiverId == user.Id))
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

    private async Task<ChatUser> GetUserByUsernameAsync(string username)
    {
        try
        {
            const string query = "SELECT Id, Username, UserType, ConnectionId FROM ChatUser WHERE Username = @Username";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ChatUser
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                UserType = reader.GetString(reader.GetOrdinal("UserType")),
                                ConnectionId = reader.IsDBNull(reader.GetOrdinal("ConnectionId")) ? null : reader.GetString(reader.GetOrdinal("ConnectionId"))
                            };
                        }
                        return null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("error");
        }
    }
}
