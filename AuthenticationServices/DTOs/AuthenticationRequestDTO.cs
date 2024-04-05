

namespace AuthenticationService.DTOs;
public class AuthenticationRequestDTO
{
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string OTP { get; set; }
    public bool IsLoginUsingOtpRequest { get; set; } = false;
    public string UserIpAddress { get; set; }
}
