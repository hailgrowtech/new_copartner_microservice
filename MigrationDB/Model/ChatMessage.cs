using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("ChatMessage")]
public class ChatMessage : BaseModel
{
    public string Contents { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid SenderId { get; set; }
    public ChatUser Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public ChatUser Receiver { get; set; }
    public string? PlanType { get; set; }
}
