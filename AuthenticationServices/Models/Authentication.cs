using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthenticationService.Models;
public class Authentication
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public string PasswordSalt { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; }
}
