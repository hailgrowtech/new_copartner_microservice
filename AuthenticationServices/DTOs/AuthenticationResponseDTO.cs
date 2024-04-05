namespace AuthenticationService.DTOs;
public class AuthenticationResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }

}
