using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Dtos;
public class ResetPasswordDTO
{
    [Required]
    public string Password { get; set; }
    public string Token { get; set; }
}

