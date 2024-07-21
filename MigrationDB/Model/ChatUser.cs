using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("ChatUser")]
public class ChatUser
{
    [Required]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? ConnectionId { get; set; }
}

