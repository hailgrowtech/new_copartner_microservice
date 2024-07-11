using CommonLibrary.CommonModels;
using MigrationDB.Models;

namespace MigrationDB.Model;
public class ChatMessage : BaseModel
{
    public string Contents { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid SenderId { get; set; }
    public ChatUser Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public ChatUser Receiver { get; set; }
}
