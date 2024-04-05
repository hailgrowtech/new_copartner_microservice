using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Models;

public class AuthenticationDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public string PasswordHash { get; set; }
}