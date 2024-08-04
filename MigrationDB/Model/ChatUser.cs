using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MigrationDB.Model;

[Table("ChatUser")]
public class ChatUser
{
    [Required]
    public Guid Id { get; set; }
   [Required]
    public string UserType { get; set; }
    public string Username { get; set; }
    public string? ConnectionId { get; set; }

    public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
    public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();
}

